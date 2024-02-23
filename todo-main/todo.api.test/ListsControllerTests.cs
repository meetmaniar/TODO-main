using Microsoft.AspNetCore.Mvc;

namespace todo.api.test
{
    public class ListsControllerTests : BaseTest
    {
        private ListsController? _controller;

        private ListsController Controller
        {
            get
            {
                if (_controller is not null) return _controller;
                
                _controller = new ListsController(_services) { TodoUser = _todoUser };
                return _controller;
            }
        }

        [Fact]
        public async Task GetLists()
        {
            var result = await Controller.GetLists();
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<TodoList[]>();

            var lists = objectResult.Value as TodoList[];
            lists.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateList()
        {
            var model = new TodoListUpdate { Name = "New List" };
            var result = await Controller.CreateList(model);
            result.Should().BeOfType<CreatedAtActionResult>();

            var createdResult = result as CreatedAtActionResult;
            createdResult.Value.Should().BeOfType<TodoList>();

            var list = createdResult.Value as TodoList;
            list.Name.Should().Be(model.Name);
        }

        [Fact]
        public async Task CreateItem()
        {
            var listsResult = (await Controller.GetLists()) as OkObjectResult;
            var list = (listsResult.Value as TodoList[]).First();
            var model = new TodoItemUpdate { Label = "New Item" };
            var result = await Controller.CreateItem(list.Id, model);
            result.Should().BeOfType<CreatedAtActionResult>();

            var createdResult = result as CreatedAtActionResult;
            createdResult.Value.Should().BeOfType<TodoItem>();

            var item = createdResult.Value as TodoItem;
            item.Label.Should().Be(model.Label);
        }

        [Fact]
        public async Task DeleteItem()
        {
            var listsResult = (await Controller.GetLists()) as OkObjectResult;
            var list = (listsResult.Value as TodoList[]).First();
            var listResponse = (await Controller.GetList(list.Id)) as OkObjectResult;
            var listDetails = listResponse.Value as TodoListDetails;
            var originalCount = listDetails.Items.Length;

            var itemId = listDetails.Items.First().Id;
            var result = await Controller.DeleteItem(list.Id, itemId);
            result.Should().BeOfType<NoContentResult>();

            listResponse = (await Controller.GetList(list.Id)) as OkObjectResult;
            listDetails = listResponse.Value as TodoListDetails;
            listDetails.Items.Should().HaveCount(originalCount - 1);

            var getItemResult = await Controller.GetItem(itemId, list.Id);
            getItemResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteList()
        {
            var listsResult = (await Controller.GetLists()) as OkObjectResult;
            var lists = listsResult.Value as TodoList[];
            var originalCount = lists.Length;
            var listId = lists[0].Id;

            var result = await Controller.DeleteList(listId);
            result.Should().BeOfType<NoContentResult>();

            listsResult = (await Controller.GetLists()) as OkObjectResult;
            lists = listsResult.Value as TodoList[];
            lists.Should().HaveCount(originalCount - 1);

            var getListResult = await Controller.GetList(listId);
            getListResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetList()
        {
            var listId = 1;
            var listResult = await Controller.GetList(listId);
            listResult.Should().BeOfType<OkObjectResult>();

            var objectResult = listResult as OkObjectResult;
            objectResult.Value.Should().BeOfType<TodoListDetails>();

            var todoList = objectResult.Value as TodoListDetails;
            todoList.Id.Should().Be(listId);
        }

        [Fact]
        public async Task GetItem()
        {
            var listId = 1;
            var itemId = 1;
            var itemResult = await Controller.GetItem(itemId, listId);
            itemResult.Should().BeOfType<OkObjectResult>();

            var objectResult = itemResult as OkObjectResult;
            objectResult.Value.Should().BeOfType<TodoItem>();

            var todoItem = objectResult.Value as TodoItem;
            todoItem.Id.Should().Be(itemId);
        }

        [Fact]
        public async Task UpdateItem()
        {
            var listId = 1;
            var itemId = 1;
            var itemResult = (await Controller.GetItem(itemId, listId)) as OkObjectResult;
            var item = itemResult.Value as TodoItem;

            var model = new TodoItemUpdate { Label = "New Name", IsComplete = !item.IsComplete };
            var result = await Controller.UpdateItem(listId, itemId, model);
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<TodoItem>();

            var updatedList = objectResult.Value as TodoItem;
            updatedList.Label.Should().Be(model.Label);
            updatedList.IsComplete.Should().Be(model.IsComplete);
        }

        [Fact]
        public async Task UpdateItem_Persisted()
        {
            var listId = 1;
            var itemId = 1;
            var itemResult = (await Controller.GetItem(itemId, listId)) as OkObjectResult;
            var item = itemResult.Value as TodoItem;

            var model = new TodoItemUpdate { Label = "New Name", IsComplete = !item.IsComplete };
            await Controller.UpdateItem(listId, itemId, model);
            _context.DetachAll(); // Clear tracked entities

            var result = (await Controller.GetItem(listId, itemId)) as OkObjectResult;
            var persistedItem = result.Value as TodoItem;
            persistedItem.Label.Should().Be(model.Label);
            persistedItem.IsComplete.Should().Be(model.IsComplete);
        }

        [Fact]
        public async Task UpdateList()
        {
            var listId = 1;
            var model = new TodoListUpdate { Name = "New Name" };
            var result = await Controller.UpdateList(listId, model);
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<TodoList>();

            var updatedList = objectResult.Value as TodoList;
            updatedList.Name.Should().Be(model.Name);
        }
    }
}

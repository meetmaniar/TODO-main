using System.Net;
using todo.models;
using todo.web.Services;

namespace todo.web.test.Util
{
    public class TestTodoClient : ITodoClient
    {
        public void Authenticate(string username) { }

        public async Task<ObjectResult<TodoItem>> CreateItem(int listId, TodoItemUpdate model) =>
            new TestObjectResult<TodoItem>(new TodoItem { Id = 1, IsComplete = model.IsComplete, Label = model.Label });

        public async Task<ObjectResult<TodoList>> CreateList(TodoListUpdate model) =>
            new TestObjectResult<TodoList>(new TodoList { Id = 1, Name = model.Name });

        public async Task<Result> DeleteItem(int id, int listId) =>
            new TestResult(HttpStatusCode.NoContent);

        public async Task<Result> DeleteList(int id) =>
            new TestResult(HttpStatusCode.NoContent);

        public async Task<ObjectResult<TodoListDetails>> GetList(int id) => 
            new TestObjectResult<TodoListDetails>(new TodoListDetails { Id = id, Items = new[] { new TodoItem { Id = id } } }, HttpStatusCode.Created);

        public async Task<ObjectResult<TodoList[]>> GetLists() =>
            new TestObjectResult<TodoList[]>(new[]
            {
                new TodoList { Id = 1 }
            }, HttpStatusCode.Created);

        public async Task<ObjectResult<TodoItem>> UpdateItem(int id, int listId, TodoItemUpdate model) =>
            new TestObjectResult<TodoItem>(new TodoItem { Id = id, IsComplete = model.IsComplete, Label = model.Label });

        public async Task<ObjectResult<TodoList>> UpdateList(int id, TodoListUpdate model) =>
            new TestObjectResult<TodoList>(new TodoList { Id = id, Name = model.Name });
    }
}

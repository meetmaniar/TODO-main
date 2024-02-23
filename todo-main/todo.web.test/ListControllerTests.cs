using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using todo.models;
using todo.web.Controllers;
using todo.web.test.Util;

namespace todo.web.test
{
    public class ListControllerTests
    {
        private ListController? _controller;

        public ListController Controller => _controller ??= new ListController(new TestTodoClient()) { TempData = new TestTempDataDictionary() };

        [Fact]
        public async Task AddItem()
        {
            var result = await Controller.AddItem(1, "New Item");
            Controller.TempData.Should().ContainKey("Success");
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().ContainKey("id");
            redirectResult.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task AddList()
        {
            var result = await Controller.AddList("New List");
            Controller.TempData.Should().ContainKey("Success");
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().ContainKey("id");
        }

        [Fact]
        public async Task DeleteItem()
        {
            var result = await Controller.DeleteItem(1, 1);
            Controller.TempData.Should().ContainKey("Success");
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().ContainKey("id");
        }

        [Fact]
        public async Task DeleteList()
        {
            var result = await Controller.DeleteList(1);
            Controller.TempData.Should().ContainKey("Success");
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetLists()
        {
            var result = await Controller.GetLists();
            result.Should().BeOfType<ViewResult>();
            
            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeOfType<TodoList[]>();
        }

        [Fact]
        public async Task GetList()
        {
            var result = await Controller.GetList(1);
            result.Should().BeOfType<ViewResult>();

            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeOfType<TodoListDetails>();
        }

        [Fact]
        public async Task ToggleItem()
        {
            var result = await Controller.ToggleItem(1, 1);
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().ContainKey("id");
            redirectResult.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task UpdateList()
        {
            var result = await Controller.UpdateList(1, "New Name");
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            redirectResult.RouteValues.Should().ContainKey("id");
            redirectResult.RouteValues["id"].Should().Be(1);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using todo.api.Extensions;
using todo.api.Services;
using todo.models;

namespace todo.api.Controllers
{
    [Route("[controller]")]
    public class ListsController : BaseController
    {
        public ListsController(TodoServices services) : base(services)
        {
        }

        [HttpPost("{listId}/items")]
        [SwaggerOperation("Add an item to a list")]
        [SwaggerResponse(201, "Returns the new item", typeof(TodoItem))]
        [SwaggerResponse(404, "If the list does not exist")]
        public async Task<IActionResult> CreateItem(int listId, [FromBody]TodoItemUpdate model)
        {
            var result = await _services.ListServices.CreateItem(listId, model, TodoUser);
            if (result is null) return NotFound();

            await _services.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = result.Id, listId }, result.AsModel());
        }

        [HttpPost("")]
        [SwaggerOperation("Create a new list")]
        [SwaggerResponse(201, "Returns the new todo list", typeof(TodoListDetails))]
        public async Task<IActionResult> CreateList([FromBody]TodoListUpdate model)
        {
            var result = await _services.ListServices.CreateList(model, TodoUser);
            await _services.SaveChangesAsync();
            return CreatedAtAction(nameof(GetList), new { id = result.Id }, result.AsModel());
        }

        [HttpDelete("{listId}/items/{id}")]
        [SwaggerOperation("Delete an item")]
        [SwaggerResponse(204, "If the item was deleted successfully")]
        [SwaggerResponse(404, "If the list or item does not exist")]
        public async Task<IActionResult> DeleteItem(int listId, int id)
        {
            await _services.ListServices.DeleteItem(id, listId, TodoUser);
            await _services.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a list")]
        [SwaggerResponse(204, "If the list was deleted successfully")]
        [SwaggerResponse(404, "If the list does not exist")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _services.ListServices.DeleteList(id, TodoUser);
            await _services.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{listId}/items/{id}")]
        [SwaggerOperation("Get a todo item")]
        [SwaggerResponse(200, "Returns the todo item", typeof(TodoItem))]
        [SwaggerResponse(404, "If the list or item do not exist")]
        public async Task<IActionResult> GetItem(int id, int listId)
        {
            var result = await _services.ListServices.GetItem(listId, id, TodoUser);
            if (result is null) return NotFound();

            return Ok(result.AsModel());
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a single todo list and its items")]
        [SwaggerResponse(200, "Returns the specified todo list", typeof(TodoListDetails))]
        [SwaggerResponse(404, "If the list does not exist")]
        public async Task<IActionResult> GetList(int id)
        {
            var entity = await _services.ListServices.GetList(id, TodoUser);
            if (entity is null) return NotFound();

            return Ok(entity.AsDetails());
        }

        [HttpGet("")]
        [SwaggerOperation("Get all todo lists")]
        [SwaggerResponse(200, "Returns a user's todo lists", typeof(TodoList[]))]
        public async Task<IActionResult> GetLists() =>
            Ok((await _services.ListServices.GetLists(TodoUser)).Select(x => x.AsModel()).ToArray());

        [HttpPut("{listId}/items/{id}")]
        [SwaggerOperation("Update the text and completed status of an item")]
        [SwaggerResponse(200, "Returns the updated item", typeof(TodoItem))]
        [SwaggerResponse(400, "If the supplied TodoItem does not pass validation")]
        [SwaggerResponse(404, "If the list or item does not exist")]
        public async Task<IActionResult> UpdateItem(int listId, int id, [FromBody]TodoItemUpdate model)
        {
            var result = await _services.ListServices.UpdateItem(id, listId, model, TodoUser);
            if (result is null) return NotFound();
            await _services.SaveChangesAsync();
            return Ok(result.AsModel());
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Update the name of a todo list")]
        [SwaggerResponse(200, "Returns the updated list", typeof(TodoList))]
        [SwaggerResponse(400, "If the supplied TodoList does not pass validation")]
        [SwaggerResponse(404, "If the list does not exist")]
        public async Task<IActionResult> UpdateList(int id, [FromBody]TodoListUpdate model)
        {
            var result = await _services.ListServices.UpdateList(id, model, TodoUser);
            if (result is null) return NotFound();

            await _services.SaveChangesAsync();
            return Ok(result.AsModel());
        }
    }
}

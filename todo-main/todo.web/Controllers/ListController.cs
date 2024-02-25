using Microsoft.AspNetCore.Mvc;
using todo.models;
using todo.web.Services;

namespace todo.web.Controllers
{
    public class ListController : BaseController
    {
        public ListController(ITodoClient client) : base(client)
        {
        }

        [HttpPost("/{culture:regex(en)}/lists/{id}/items/add")]
        [HttpPost("/{culture:regex(fr)}/listes/{id}/elements/ajouter")]
        public async Task<IActionResult> AddItem(int id, string label)
        {
            var result = await _client.CreateItem(id, new TodoItemUpdate { Label = label });
            if (!result.IsSuccessStatusCode)
                throw new ResultException($"Unable to add item to list {id} for {UserName}", result);

            SetSuccessMessage();
            return RedirectToAction(nameof(GetList), new { id });
        }

        [HttpPost("/{culture:regex(en)}/lists/add")]
        [HttpPost("/{culture:regex(fr)}/listes/ajouter")]
        public async Task<IActionResult> AddList(string name)
        {
            var result = await _client.CreateList(new TodoListUpdate { Name = name });
            if (!result.IsSuccessStatusCode)
                throw new ResultException($"Unable to create list \"{name}\" for {UserName}", result);

            SetSuccessMessage();
            return RedirectToAction(nameof(GetList), new { id = result.Value.Id });
        }

        [HttpGet("/{culture:regex(en)}/lists/{listId}/items/{itemId}/delete")]
        [HttpGet("/{culture:regex(fr)}/listes/{listId}/elements/{itemId}/supprimer")]
        public async Task<IActionResult> DeleteItem(int listId, int itemId)
        {
            var result = await _client.DeleteItem(itemId, listId);  // Deleting the item.
            SetSuccessMessage();    // Setting the success message.
            return RedirectToAction(nameof(GetList), new { id = itemId });  // Returning the Task<IActionResult> with id of the deleted item.
        }

        [HttpGet("/{culture:regex(en)}/lists/{id}/delete")]
        [HttpGet("/{culture:regex(fr)}/listes/{id}/supprimer")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var result = await _client.DeleteList(id);  // Deleting the list.
            SetSuccessMessage();    // Setting the success message.
            return RedirectToAction(nameof(GetList)); // Returning the Task<IActionResult>.
        }

        [HttpGet("/{culture:regex(en)}/lists")]
        [HttpGet("/{culture:regex(fr)}/listes")]
        public async Task<IActionResult> GetLists()
        {
            var result = await _client.GetLists();
            return View("Lists", result.Value);
        }

        [HttpGet("/{culture:regex(en)}/lists/{id}")]
        [HttpGet("/{culture:regex(fr)}/listes/{id}")]
        public async Task<IActionResult> GetList(int id)
        {
            var result = await _client.GetList(id);
            return View("List", result.Value);
        }

        [HttpGet("/{culture:regex(en)}/lists/{id}/items/{itemId}/toggle")]
        [HttpGet("/{culture:regex(fr)}/lists/{id}/elements/{itemId}/changer")]
        public async Task<IActionResult> ToggleItem(int id, int itemId)
        {
            var listResult = await _client.GetList(id);
            var item = listResult.Value.Items.Single(x => x.Id == itemId);
            var result = await _client.UpdateItem(itemId, id, new TodoItemUpdate { Label = item.Label, IsComplete = !item.IsComplete });
            if (!result.IsSuccessStatusCode)
                throw new ResultException($"Unable to edit item {itemId} for {UserName}", result);

            SetSuccessMessage();
            return RedirectToAction(nameof(GetList), new { id });
        }

        [HttpPost("/{culture:regex(en)}/lists/{id}")]
        [HttpPost("/{culture:regex(fr)}/listes/{id}")]
        public async Task<IActionResult> UpdateList(int id, string name)
        {
            var result = await _client.UpdateList(id, new TodoListUpdate { Name = name });
            if (!result.IsSuccessStatusCode)
                throw new ResultException($"Unable to edit list {id} for {UserName}", result);

            SetSuccessMessage();
            return RedirectToAction(nameof(GetList), new { id });
        }
    }
}

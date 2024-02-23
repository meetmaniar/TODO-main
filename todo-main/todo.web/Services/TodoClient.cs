using todo.models;

namespace todo.web.Services
{
    public class TodoClient : ITodoClient
    {
        private readonly HttpClient _client;

        public TodoClient(HttpClient client)
        {
            _client = client;
            client.DefaultRequestHeaders.Add(AppSettings.Instance.ApiKeyHeader, AppSettings.Instance.ApiKey);
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.BaseAddress = new Uri(AppSettings.Instance.ApiUrl);
        }

        public void Authenticate(string username) =>
            _client.DefaultRequestHeaders.Add(AppSettings.Instance.TodoUserHeader, username);

        public async Task<ObjectResult<TodoItem>> CreateItem(int listId, TodoItemUpdate model) =>
            await PostAsync<TodoItemUpdate, TodoItem>($"lists/{listId}/items", model);

        public async Task<ObjectResult<TodoList>> CreateList(TodoListUpdate model) =>
            await PostAsync<TodoListUpdate, TodoList>("lists", model);

        public async Task<Result> DeleteItem(int id, int listId) =>
            await DeleteAsync($"lists/{listId}/items/{id}");

        public async Task<Result> DeleteList(int id) =>
            await DeleteAsync($"lists/{id}");

        public async Task<ObjectResult<TodoListDetails>> GetList(int id) =>
            await GetAsync<TodoListDetails>($"lists/{id}");

        public async Task<ObjectResult<TodoList[]>> GetLists() =>
            await GetAsync<TodoList[]>("lists");

        public async Task<ObjectResult<TodoItem>> UpdateItem(int id, int listId, TodoItemUpdate model) =>
            await PutAsync<TodoItemUpdate, TodoItem>($"lists/{listId}/items/{id}", model);

        public async Task<ObjectResult<TodoList>> UpdateList(int id, TodoListUpdate model) =>
            await PutAsync<TodoListUpdate, TodoList>($"lists/{id}", model);

        private async Task<Result> DeleteAsync(string url)
        {
            var response = await _client.DeleteAsync(url);
            return await Result.Read(response);
        }

        private async Task<ObjectResult<T>> GetAsync<T>(string url) where T : class
        {
            var response = await _client.GetAsync(url);
            return await ObjectResult<T>.ReadObject(response);
        }

        private async Task<ObjectResult<TResult>> PostAsync<TModel, TResult>(string url, TModel model) 
            where TResult : class 
            where TModel : class
        {
            var response = await _client.PostAsJsonAsync(url, model);
            return await ObjectResult<TResult>.ReadObject(response);
        }

        private async Task<ObjectResult<TResult>> PutAsync<TModel, TResult>(string url, TModel model) 
            where TResult : class 
            where TModel : class
        {
            var response = await _client.PutAsJsonAsync(url, model);
            return await ObjectResult<TResult>.ReadObject(response);
        }
    }
}

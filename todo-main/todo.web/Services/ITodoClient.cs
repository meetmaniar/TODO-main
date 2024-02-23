using todo.models;

namespace todo.web.Services
{
    public interface ITodoClient
    {
        void Authenticate(string username);

        Task<ObjectResult<TodoItem>> CreateItem(int listId, TodoItemUpdate model);

        Task<ObjectResult<TodoList>> CreateList(TodoListUpdate model);

        Task<Result> DeleteItem(int id, int listId);

        Task<Result> DeleteList(int id);

        Task<ObjectResult<TodoListDetails>> GetList(int id);

        Task<ObjectResult<TodoList[]>> GetLists();

        Task<ObjectResult<TodoList>> UpdateList(int id, TodoListUpdate model);

        Task<ObjectResult<TodoItem>> UpdateItem(int id, int listId,  TodoItemUpdate model);
    }
}

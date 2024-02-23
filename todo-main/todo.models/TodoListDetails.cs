namespace todo.models
{
    public class TodoListDetails : TodoList
    {
        public TodoItem[] Items { get; set; } = Array.Empty<TodoItem>();
    }
}

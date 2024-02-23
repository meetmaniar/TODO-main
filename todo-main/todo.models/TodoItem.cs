namespace todo.models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public bool IsComplete { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace todo.models
{
    public class TodoItemUpdate
    {
        [Required]
        public string Label { get; set; } = "";

        public bool IsComplete { get; set; } = false;
    }
}

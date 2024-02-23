using System.ComponentModel.DataAnnotations;

namespace todo.models
{
    public class TodoListUpdate
    {
        [Required]
        public string Name { get; set; } = "";
    }
}

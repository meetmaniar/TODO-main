using System.ComponentModel.DataAnnotations;

namespace todo.data.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public int ListId { get; set; }

        [Required]
        public string Label { get; set; } = "";

        public bool IsComplete { get; set; } = false;

        public List? List { get; set; }
    }
}

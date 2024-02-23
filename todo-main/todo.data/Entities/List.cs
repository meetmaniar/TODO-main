using System.ComponentModel.DataAnnotations;

namespace todo.data.Entities
{
    public class List
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public int UserId { get; set; }

        public TodoUser? User { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}

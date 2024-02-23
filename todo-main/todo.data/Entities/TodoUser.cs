using System.ComponentModel.DataAnnotations;

namespace todo.data.Entities
{
    public class TodoUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = "";

        public ICollection<List>? Lists { get; set; }
    }
}

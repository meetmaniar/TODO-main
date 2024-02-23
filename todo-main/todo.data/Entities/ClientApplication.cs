using System.ComponentModel.DataAnnotations;

namespace todo.data.Entities
{
    public class ClientApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ApiKey { get; set; } = Guid.NewGuid().ToString();

        public bool IsActive { get; set; } = true;
    }
}

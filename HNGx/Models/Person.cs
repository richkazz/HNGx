using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HNGx.Models
{
    [Table("persons")]
    public class Person
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
    public class PersonRequest
    {
        [Required(ErrorMessage = "Name attribute is required")]
        public string Name { get; set; } = null!;
    }
}

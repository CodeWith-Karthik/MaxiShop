using System.ComponentModel.DataAnnotations;

namespace MaxiShop.Web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
    }
}

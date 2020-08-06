using System.ComponentModel.DataAnnotations;

namespace EShopAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(60, ErrorMessage="Limited between 3 and 60 characters")]
        [MinLength(3, ErrorMessage="Limited between 3 and 60 characters")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage="Limited between 3 and 1024 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage="Price can't be zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage="Category can't be zero")]
        public int CategoryId { get; set; }
        //propriedade de referencia
        public Category Category { get; set; }
    }
}
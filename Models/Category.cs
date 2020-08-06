
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.Models
{
    // [Table("Category")]
    public class Category
    {
        [Key]
        // [Column("id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(60, ErrorMessage="Must be between 3 and 60 characters")]
        [MinLength(3, ErrorMessage="Must be between 3 and 60 characters")]
        public string Title { get; set; }
    }
}
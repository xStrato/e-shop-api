using System.ComponentModel.DataAnnotations;

namespace EShopAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20, ErrorMessage="Limited between 3 and 20 characters")]
        [MinLength(3, ErrorMessage="Limited between 3 and 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20, ErrorMessage="Limited between 3 and 20 characters")]
        [MinLength(3, ErrorMessage="Limited between 3 and 20 characters")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
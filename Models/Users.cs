using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }

        public List<Orders> Orders { get; set; }
    }
}

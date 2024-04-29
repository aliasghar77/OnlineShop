using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class AccountViewModel
    {
        
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        public string Email { get; set; }

       
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        public string Password { get; set; }

        
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Password")]
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        public string Email { get; set; }


        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Company.Models.ViewModels
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "Enter Email Address")]
        //[EmailAddress]
        //public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

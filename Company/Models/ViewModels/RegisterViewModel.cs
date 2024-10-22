using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [Compare("Name", ErrorMessage = "Name and Confirm not match")]
        public string ConfirmName { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email and Confirm not match")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Pass and Confirm not match")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Is Company?")]
        public bool IsSelected { get; set; }    

        //public string Mobile { get; set; }
    }
}

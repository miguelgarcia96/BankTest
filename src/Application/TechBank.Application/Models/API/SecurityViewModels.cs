using System.ComponentModel.DataAnnotations;

namespace TechBank.Application.Models.API
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
    }
}

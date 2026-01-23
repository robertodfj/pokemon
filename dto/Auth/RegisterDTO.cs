using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto.auth
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // El username sera el email sin . y hasta el @

        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 13)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "User";

        public RegisterDTO(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        
    }
}
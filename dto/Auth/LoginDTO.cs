using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto.auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public LoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
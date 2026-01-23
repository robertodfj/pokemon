using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto.auth
{
    public class LoginDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 13)]
        public string Password { get; set; }

        public LoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
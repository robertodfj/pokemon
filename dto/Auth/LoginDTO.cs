using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto.auth
{
    public class LoginDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
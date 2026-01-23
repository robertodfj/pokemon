using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Pokemon.data;
using Pokemon.dto.auth;

namespace Pokemon.service.auth
{
    public class AuthService
    {
        private readonly AppDBContext _context;

        public AuthService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(RegisterDTO registerDTO)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == registerDTO.Email);
            if (userExists)
            {
                throw new Exception("User already exists");
            }
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }
            var user = new model.User
            {
                Email = registerDTO.Email,
                UserName = Regex.Replace(registerDTO.Email.Split('@')[0], @"[^a-zA-Z0-9]", ""),
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                Role = "User"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
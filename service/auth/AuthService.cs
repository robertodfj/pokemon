using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Pokemon.data;
using Pokemon.dto.auth;
using Pokemon.model;
using Pokemon.token;

namespace Pokemon.service.auth
{
    public class AuthService
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            var user = new User
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

        public async Task<string> LoginUser(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Invalid email or password");
            }
            return new GenerateToken().CreateToken(user, _configuration);
        }
    }
}
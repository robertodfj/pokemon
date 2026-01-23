using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokemon.dto.auth;
using Pokemon.service.auth;

namespace Pokemon.controller.auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            _logger.LogInformation("Attempting to log in user: {Email}", loginDto.Email);

            var token = await _authService.LoginUser(loginDto);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            _logger.LogInformation("Attempting to register user: {Email}", registerDto.Email);

            await _authService.RegisterUser(registerDto);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
        {
            _logger.LogInformation("Attempting to register admin: {Email}", registerDto.Email);

            await _authService.RegisterAdmin(registerDto);

            return Ok(new { message = "Admin registered successfully" });
        }
    }
}
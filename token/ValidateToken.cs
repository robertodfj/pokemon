using Microsoft.IdentityModel.Tokens;

namespace Pokemon.token
{
    public class ValidateToken
    {
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])
                ),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
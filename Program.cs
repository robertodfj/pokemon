using Microsoft.EntityFrameworkCore;
using Pokemon.data;
using Pokemon.service;
using Pokemon.service.auth;
using Pokemon.token;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Registrar APP DBContext
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PokemonService>();
builder.Services.AddScoped<GenerateToken>();

// Añadimos el JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearerDefaults.AuthenticationScheme";
    options.DefaultChallengeScheme = "JwtBearerDefaults.AuthenticationScheme";
}).AddJwtBearer("Jwt ", options =>
{
    options.TokenValidationParameters = ValidateToken.GetTokenValidationParameters(builder.Configuration);
});

// Añadimos la autorizacion
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Añadimos el Seed Data



var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();



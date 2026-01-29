using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = ValidateToken.GetTokenValidationParameters(builder.Configuration);
});

var app = builder.Build();

// Usar el seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDBContext>();
    SeedData seedData = new SeedData(context, services.GetRequiredService<ILogger<SeedData>>());
    seedData.SeedDatabase();
}
app.UseMiddleware<Exception>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();



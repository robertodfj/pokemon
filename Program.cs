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



var app = builder.Build();



app.Run();


// Crear el program cs
// Crear las migraciones
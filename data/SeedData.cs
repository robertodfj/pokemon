using Pokemon.model;

namespace Pokemon.data
{
    public class SeedData
    {
        private readonly AppDBContext _context;
        private readonly ILogger<SeedData> _logger;

        public SeedData(AppDBContext context, ILogger<SeedData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Creamos seed Data base, solo tenemos un metodo en este momento pero asi solo tendremos que añadir aqui l metodo sin nada mas
        public void SeedDatabase()
        {
            CreateFirstAdmin();
        }

        public void CreateFirstAdmin()
        {
            var adminExists = _context.Users.Any(u => u.Role == "Admin");

            if (!adminExists)
            {
                var adminUser = new User
                {
                    Email = "admin@pokemon.com",
                    UserName = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                };

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                _logger.LogInformation("First admin user created.");
            }
            else
            {
                _logger.LogInformation("Admin user already exists.");
            }
        }
    }
}
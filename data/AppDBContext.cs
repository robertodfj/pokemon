using Microsoft.EntityFrameworkCore;

namespace Pokemon.data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<model.Pokemon> Pokemons { get; set; }
        public DbSet<model.User> Users { get; set; }
    }
}
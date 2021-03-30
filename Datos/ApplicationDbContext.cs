using Entidades.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Datos.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string connectionString;

        public ApplicationDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<PersonasFisicasEModel> PersonasFisicas { get; set; }




    }
}

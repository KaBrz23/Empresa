using Empresa.Models;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da chave estrangeira
            modelBuilder.Entity<Empregado>()
                .HasOne(e => e.Departamento)
                .WithMany()
                .HasForeignKey(e => e.DepId);
        }
    }
}

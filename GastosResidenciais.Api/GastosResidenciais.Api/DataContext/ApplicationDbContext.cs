using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.DataContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<PessoaModel> Pessoas { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<TransacaoModel> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransacaoModel>()
                .Property(t => t.Valor)
                .HasPrecision(18, 2); 
        }
    }
}

using Cidades.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Cidades.Persistencia.Contexto
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Cidade> Cidades { get; set; }

        public DbSet<Fronteira> Fronteiras { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Cidade>()
            //    .HasOne(c => c.Fronteira)
            //    .WithMany(c => c.Fronteiras)
            //    .HasForeignKey(c => c.CidadeId);

            modelBuilder.Entity<Cidade>()
                .HasMany(c => c.Fronteiras)
                .WithOne(f => f.Cidade)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

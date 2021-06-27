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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Cidade>()
            //    .HasOne(c => c.Fronteira)
            //    .WithMany(c => c.Fronteiras)
            //    .HasForeignKey(c => c.CidadeId);

            modelBuilder.Entity<Fronteira>()
                .HasKey(f => new { f.CidadeOrigemId, f.CidadeFronteiraId });
            modelBuilder.Entity<Fronteira>()
                .HasOne(f => f.CidadeOrigem)
                .WithMany(c => c.Fronteiras)
                .HasForeignKey(f => f.CidadeOrigemId);
            modelBuilder.Entity<Fronteira>()
                .HasOne(f => f.CidadeFronteira)
                .WithMany(c => c.Origens)
                .HasForeignKey(f => f.CidadeFronteiraId);
        }
    }
}

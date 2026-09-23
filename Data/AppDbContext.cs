using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Interacao> Interacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Garante que se uma categoria for excluída, não delete os chamados dela por acidente (Restrict)
            modelBuilder.Entity<Chamado>()
                .HasOne(c => c.Categoria)
                .WithMany(cat => cat.Chamados)
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Se um chamado for deletado, apaga todas as interações dele automaticamente (Cascade)
            modelBuilder.Entity<Interacao>()
                .HasOne(i => i.Chamado)
                .WithMany(c => c.Interacoes)
                .HasForeignKey(i => i.ChamadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

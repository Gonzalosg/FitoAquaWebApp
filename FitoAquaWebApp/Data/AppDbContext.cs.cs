// Data/AppDbContext.cs
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<UsuarioObra> UsuarioObras { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Albaran> Albaranes { get; set; }
        public DbSet<DetalleAlbaran> DetallesAlbaran { get; set; }
        public DbSet<ParteAveria> PartesAveria { get; set; }
        public DbSet<FotoEstadoObra> FotosEstadoObra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Claves compuestas
            modelBuilder.Entity<UsuarioObra>()
                .HasKey(uo => new { uo.UsuarioId, uo.ObraId });

            modelBuilder.Entity<DetalleAlbaran>()
                .HasKey(da => new { da.AlbaranId, da.MaterialId });

            // Relaciones para UsuarioObra
            modelBuilder.Entity<UsuarioObra>()
                .HasOne(uo => uo.Usuario)
                .WithMany()
                .HasForeignKey(uo => uo.UsuarioId);

            modelBuilder.Entity<UsuarioObra>()
                .HasOne(uo => uo.Obra)
                .WithMany(o => o.Asignaciones)
                .HasForeignKey(uo => uo.ObraId);

            // Relaciones para DetalleAlbaran
            modelBuilder.Entity<DetalleAlbaran>()
                .HasOne(da => da.Albaran)
                .WithMany(a => a.Detalles)
                .HasForeignKey(da => da.AlbaranId);

            modelBuilder.Entity<DetalleAlbaran>()
                .HasOne(da => da.Material)
                .WithMany(m => m.DetallesAlbaran)
                .HasForeignKey(da => da.MaterialId);
        }
    }
}

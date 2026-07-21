// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using TarijaReadApp.Models;

namespace TarijaReadApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Ejemplar> Ejemplares { get; set; }
    public DbSet<Socio> Socios { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<Multa> Multas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación 1:1 Prestamo-Multa (evita ciclo de eliminación en cascada)
        modelBuilder.Entity<Multa>()
            .HasOne(m => m.Prestamo)
            .WithOne(p => p.Multa)
            .HasForeignKey<Multa>(m => m.PrestamoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Evitar cascada múltiple en Prestamo (Ejemplar y Socio)
        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Ejemplar)
            .WithMany(e => e.Prestamos)
            .HasForeignKey(p => p.EjemplarId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Socio)
            .WithMany(s => s.Prestamos)
            .HasForeignKey(p => p.SocioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Prestamos)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Precisión decimal para dinero (ODS 8)
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in properties)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
    }
}
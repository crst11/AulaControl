using AulaControl.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AulaControl.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Estudiante>().ToTable("estudiantes");
        modelBuilder.Entity<Equipo>().ToTable("equipos");
        modelBuilder.Entity<Prestamo>().ToTable("prestamos");

        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.CodigoEstudiante)
            .IsUnique();

        modelBuilder.Entity<Equipo>()
            .HasIndex(e => e.CodigoInventario)
            .IsUnique();

        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Estudiante)
            .WithMany(e => e.Prestamos)
            .HasForeignKey(p => p.EstudianteId);

        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Equipo)
            .WithMany(e => e.Prestamos)
            .HasForeignKey(p => p.EquipoId);
    }
}
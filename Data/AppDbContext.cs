using Microsoft.EntityFrameworkCore;
using SalonReservaApi.Models;

namespace SalonReservaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Salon> Salones => Set<Salon>();
    public DbSet<Reserva> Reservas => Set<Reserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Salon>().ToTable("salones");
        modelBuilder.Entity<Reserva>().ToTable("reservas");

        modelBuilder.Entity<Salon>().HasData(
            new Salon { Id = 1, Nombre = "Salón A" },
            new Salon { Id = 2, Nombre = "Salón B" },
            new Salon { Id = 3, Nombre = "Salón C" }
        );
    }

}
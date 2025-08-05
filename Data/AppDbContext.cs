using Microsoft.EntityFrameworkCore;
using SalonReservaApi.Models;

namespace SalonReservaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Salon> Salones { get; set; }
}

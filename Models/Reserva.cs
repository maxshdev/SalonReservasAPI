using System.ComponentModel.DataAnnotations;

namespace SalonReservaApi.Models;

public class Reserva
{
    public int Id { get; set; }

    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    public TimeOnly HoraInicio { get; set; }

    [Required]
    public TimeOnly HoraFin { get; set; }

    [Required]
    public int SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;
}
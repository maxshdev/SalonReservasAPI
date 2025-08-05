namespace SalonReservaApi.Models;

public class Reserva
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }

    public int SalonId { get; set; }
    public Salon Salon { get; set; } = default!;
}

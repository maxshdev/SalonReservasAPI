namespace SalonReservaApi.Models;

public class Salon
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
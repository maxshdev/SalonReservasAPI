using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonReservaApi.Data;
using SalonReservaApi.Models;

namespace SalonReservaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearReserva([FromBody] Reserva reserva)
    {
        // Validar que HoraInicio sea anterior a HoraFin
        if (reserva.HoraInicio >= reserva.HoraFin)
            return BadRequest("La hora de inicio debe ser anterior a la de fin.");

        // Definir horario permitido: 9:00 a 18:00
        var apertura = new TimeOnly(9, 0);
        var cierre = new TimeOnly(18, 0);

        if (reserva.HoraInicio < apertura || reserva.HoraFin > cierre)
            return BadRequest("La reserva debe estar entre las 9:00 y las 18:00.");

        // Validar solapamientos con margen de 30 minutos
        var solapamiento = await _context.Reservas
            .Where(r => r.Fecha == reserva.Fecha && r.SalonId == reserva.SalonId)
            .AnyAsync(r =>
                reserva.HoraInicio < r.HoraFin.AddMinutes(30) &&
                reserva.HoraFin > r.HoraInicio.AddMinutes(-30)
            );

        if (solapamiento)
            return Conflict("Ya existe una reserva que se superpone con este horario o no respeta los 30 minutos de margen.");

        // Si pasa validaciones, agregar y guardar
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        // Devolver DTO (por consistencia)
        var reservaDto = new ReservaDto
        {
            Id = reserva.Id,
            Fecha = reserva.Fecha,
            HoraInicio = reserva.HoraInicio,
            HoraFin = reserva.HoraFin,
            SalonId = reserva.SalonId,
            SalonNombre = (await _context.Salones.FindAsync(reserva.SalonId))?.Nombre ?? ""
        };

        return Ok(reservaDto);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodasLasReservas()
    {
        var reservas = await _context.Reservas
            .Include(r => r.Salon)
            .Select(r => new ReservaDto
            {
                Id = r.Id,
                Fecha = r.Fecha,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                SalonId = r.SalonId,
                SalonNombre = r.Salon.Nombre
            })
            .ToListAsync();

        return Ok(reservas);
    }

    [HttpGet("{fecha}")]
    public async Task<IActionResult> ObtenerReservas(DateOnly fecha)
    {
        var reservas = await _context.Reservas
            .Include(r => r.Salon)
            .Where(r => r.Fecha == fecha)
            .Select(r => new ReservaDto
            {
                Id = r.Id,
                Fecha = r.Fecha,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                SalonId = r.SalonId,
                SalonNombre = r.Salon.Nombre
            })
            .ToListAsync();

        return Ok(reservas);
    }
}

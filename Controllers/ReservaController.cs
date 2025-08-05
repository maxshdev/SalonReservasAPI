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
    public async Task<IActionResult> Crear([FromBody] Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        return Ok(reserva);
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

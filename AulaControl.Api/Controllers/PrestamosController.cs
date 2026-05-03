using AulaControl.Api.Data;
using AulaControl.Api.DTOs;
using AulaControl.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AulaControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PrestamosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
    {
        return await _context.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Equipo)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    [HttpGet("activos")]
    public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamosActivos()
    {
        return await _context.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Equipo)
            .Where(p => p.EstadoPrestamo == "Activo")
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    [HttpGet("vencidos")]
    public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamosVencidos()
    {
        return await _context.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Equipo)
            .Where(p => p.EstadoPrestamo == "Activo" &&
                        p.FechaDevolucionEstimada < DateTime.Now)
            .OrderBy(p => p.FechaDevolucionEstimada)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Prestamo>> CrearPrestamo(PrestamoCreateDto dto)
    {
        var estudiante = await _context.Estudiantes.FindAsync(dto.EstudianteId);

        if (estudiante == null)
            return BadRequest("El estudiante no existe.");

        var equipo = await _context.Equipos.FindAsync(dto.EquipoId);

        if (equipo == null)
            return BadRequest("El equipo no existe.");

        if (equipo.Estado != "Disponible")
            return BadRequest("El equipo no está disponible para préstamo.");

        var prestamo = new Prestamo
        {
            EstudianteId = dto.EstudianteId,
            EquipoId = dto.EquipoId,
            FechaPrestamo = DateTime.Now,
            FechaDevolucionEstimada = dto.FechaDevolucionEstimada,
            EstadoPrestamo = "Activo",
            Observacion = dto.Observacion
        };

        equipo.Estado = "Prestado";

        _context.Prestamos.Add(prestamo);
        await _context.SaveChangesAsync();

        return Ok(prestamo);
    }

    [HttpPut("{id}/devolver")]
    public async Task<IActionResult> DevolverPrestamo(int id)
    {
        var prestamo = await _context.Prestamos
            .Include(p => p.Equipo)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prestamo == null)
            return NotFound("Préstamo no encontrado.");

        if (prestamo.EstadoPrestamo == "Devuelto")
            return BadRequest("Este préstamo ya fue devuelto.");

        prestamo.EstadoPrestamo = "Devuelto";
        prestamo.FechaDevolucionReal = DateTime.Now;

        if (prestamo.Equipo != null)
            prestamo.Equipo.Estado = "Disponible";

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
using AulaControl.Api.Data;
using AulaControl.Api.DTOs;
using AulaControl.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AulaControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquiposController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
    {
        return await _context.Equipos
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    [HttpGet("disponibles")]
    public async Task<ActionResult<IEnumerable<Equipo>>> GetEquiposDisponibles()
    {
        return await _context.Equipos
            .Where(e => e.Estado == "Disponible")
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Equipo>> GetEquipo(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);

        if (equipo == null)
            return NotFound("Equipo no encontrado.");

        return equipo;
    }

    [HttpPost]
    public async Task<ActionResult<Equipo>> CrearEquipo(EquipoCreateDto dto)
    {
        var existeCodigo = await _context.Equipos
            .AnyAsync(e => e.CodigoInventario == dto.CodigoInventario);

        if (existeCodigo)
            return BadRequest("Ya existe un equipo con ese código de inventario.");

        var equipo = new Equipo
        {
            Nombre = dto.Nombre,
            CodigoInventario = dto.CodigoInventario,
            TipoEquipo = dto.TipoEquipo,
            Marca = dto.Marca,
            Estado = "Disponible",
            FechaRegistro = DateTime.Now
        };

        _context.Equipos.Add(equipo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id }, equipo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarEquipo(int id, EquipoCreateDto dto)
    {
        var equipo = await _context.Equipos.FindAsync(id);

        if (equipo == null)
            return NotFound("Equipo no encontrado.");

        equipo.Nombre = dto.Nombre;
        equipo.CodigoInventario = dto.CodigoInventario;
        equipo.TipoEquipo = dto.TipoEquipo;
        equipo.Marca = dto.Marca;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> CambiarEstadoEquipo(int id, string estado)
    {
        var equipo = await _context.Equipos.FindAsync(id);

        if (equipo == null)
            return NotFound("Equipo no encontrado.");

        equipo.Estado = estado;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarEquipo(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);

        if (equipo == null)
            return NotFound("Equipo no encontrado.");

        _context.Equipos.Remove(equipo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
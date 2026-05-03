using AulaControl.Api.Data;
using AulaControl.Api.DTOs;
using AulaControl.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AulaControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstudiantesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
    {
        return await _context.Estudiantes
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound("Estudiante no encontrado.");
        }

        return estudiante;
    }

    [HttpPost]
    public async Task<ActionResult<Estudiante>> CrearEstudiante(EstudianteCreateDto dto)
    {
        var existeCodigo = await _context.Estudiantes
            .AnyAsync(e => e.CodigoEstudiante == dto.CodigoEstudiante);

        if (existeCodigo)
        {
            return BadRequest("Ya existe un estudiante con ese código.");
        }

        var estudiante = new Estudiante
        {
            Nombre = dto.Nombre,
            CodigoEstudiante = dto.CodigoEstudiante,
            Correo = dto.Correo,
            Programa = dto.Programa,
            Telefono = dto.Telefono,
            FechaRegistro = DateTime.Now
        };

        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarEstudiante(int id, EstudianteCreateDto dto)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound("Estudiante no encontrado.");
        }

        var existeCodigo = await _context.Estudiantes
            .AnyAsync(e => e.CodigoEstudiante == dto.CodigoEstudiante && e.Id != id);

        if (existeCodigo)
        {
            return BadRequest("Ya existe otro estudiante con ese código.");
        }

        estudiante.Nombre = dto.Nombre;
        estudiante.CodigoEstudiante = dto.CodigoEstudiante;
        estudiante.Correo = dto.Correo;
        estudiante.Programa = dto.Programa;
        estudiante.Telefono = dto.Telefono;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound("Estudiante no encontrado.");
        }

        var tienePrestamoActivo = await _context.Prestamos
            .AnyAsync(p => p.EstudianteId == id && p.EstadoPrestamo == "Activo");

        if (tienePrestamoActivo)
        {
            return BadRequest("No se puede eliminar este estudiante porque tiene un préstamo activo. Primero debe devolver o saldar el préstamo.");
        }

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
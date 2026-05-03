namespace AulaControl.Api.Models;

public class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string CodigoEstudiante { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Programa { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public List<Prestamo> Prestamos { get; set; } = new();
}
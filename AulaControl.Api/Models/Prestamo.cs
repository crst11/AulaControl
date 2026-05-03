namespace AulaControl.Api.Models;

public class Prestamo
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }
    public Estudiante? Estudiante { get; set; }

    public int EquipoId { get; set; }
    public Equipo? Equipo { get; set; }

    public DateTime FechaPrestamo { get; set; } = DateTime.Now;

    public DateTime FechaDevolucionEstimada { get; set; }

    public DateTime? FechaDevolucionReal { get; set; }

    public string EstadoPrestamo { get; set; } = "Activo";

    public string Observacion { get; set; } = string.Empty;
}
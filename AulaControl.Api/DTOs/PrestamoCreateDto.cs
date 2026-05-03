namespace AulaControl.Api.DTOs;

public class PrestamoCreateDto
{
    public int EstudianteId { get; set; }

    public int EquipoId { get; set; }

    public DateTime FechaDevolucionEstimada { get; set; }

    public string Observacion { get; set; } = string.Empty;
}
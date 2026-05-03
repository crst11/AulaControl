namespace AulaControl.Api.DTOs;

public class EstudianteCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string CodigoEstudiante { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Programa { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
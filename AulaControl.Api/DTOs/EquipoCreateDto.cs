namespace AulaControl.Api.DTOs;

public class EquipoCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string CodigoInventario { get; set; } = string.Empty;
    public string TipoEquipo { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
}
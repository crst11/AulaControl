namespace AulaControl.Api.Models;

public class Equipo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string CodigoInventario { get; set; } = string.Empty;

    public string TipoEquipo { get; set; } = string.Empty;

    public string Marca { get; set; } = string.Empty;

    public string Estado { get; set; } = "Disponible";

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public List<Prestamo> Prestamos { get; set; } = new();
}
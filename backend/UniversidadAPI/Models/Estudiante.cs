namespace UniversidadAPI.Models;

public class Estudiante
{
    public required string Id { get; set; }
    public required string Nombre { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public decimal DeudaPendiente { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = "Activo"; // Activo, Inactivo, Graduado
    public int Semestre { get; set; }
}

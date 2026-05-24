namespace InternetAPI.Models;

public class Cliente
{
    public required string Id { get; set; }
    public required string Nombre { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = "Activo"; // Activo, Inactivo, Suspendido
}

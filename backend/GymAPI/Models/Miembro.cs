namespace GymAPI.Models;

public class Miembro
{
    public required string Id { get; set; }
    public required string Nombre { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public decimal DeudaPendiente { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = "Activo"; // Activo, Inactivo, Suspendido
    public string TipoMembresia { get; set; } = "Básica"; // Básica, Premium, VIP
}

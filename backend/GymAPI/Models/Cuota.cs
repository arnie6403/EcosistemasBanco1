namespace GymAPI.Models;

public class Cuota
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string MiembroId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = "Pendiente"; // Pendiente, Pagada
    public DateTime? FechaPago { get; set; }
    public string? ReferenciaTransaccion { get; set; }
}

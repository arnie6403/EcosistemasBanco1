namespace UniversidadAPI.Models;

public class Matricula
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string EstudianteId { get; set; }
    public decimal Monto { get; set; }
    public int Semestre { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = "Pendiente"; // Pendiente, Pagada
    public DateTime? FechaPago { get; set; }
    public string? ReferenciaTransaccion { get; set; }
}

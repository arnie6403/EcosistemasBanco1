namespace InternetAPI.Models;

public class Contrato
{
    public required string Id { get; set; }
    public required string ClienteId { get; set; }
    public required string Nombre { get; set; }
    public decimal DeudaPendiente { get; set; }
    public DateTime FechaContratacion { get; set; }
    public string Velocidad { get; set; } = "25Mbps"; // 25Mbps, 50Mbps, 100Mbps, etc
    public string Estado { get; set; } = "Activo";
    public decimal MontoMensual { get; set; } = 25m;
}

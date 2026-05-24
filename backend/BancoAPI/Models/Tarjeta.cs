namespace BancoAPI.Models;

public class Tarjeta
{
    public required string Numero { get; set; }
    public required string ClienteId { get; set; }
    public required string Tipo { get; set; } // Debito o Credito
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = "Activa";
    public decimal LimiteCredito { get; set; }
}

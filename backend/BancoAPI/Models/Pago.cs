namespace BancoAPI.Models;

public class Pago
{
    public required string Referencia { get; set; }
    public required string NumeroTarjeta { get; set; }
    public decimal Monto { get; set; }
    public decimal Comision { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Exitoso";
    public string? Descripcion { get; set; }
}

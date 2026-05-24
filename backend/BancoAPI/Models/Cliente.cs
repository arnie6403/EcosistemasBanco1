namespace BancoAPI.Models;

public class Cliente
{
    public required string Id { get; set; }
    public required string Nombre { get; set; }
    public decimal Saldo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = "Activo";
}

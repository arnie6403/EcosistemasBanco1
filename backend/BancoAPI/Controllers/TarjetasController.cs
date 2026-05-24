using Microsoft.AspNetCore.Mvc;
using BancoAPI.Models;
using BancoAPI.DTOs;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarjetasController : ControllerBase
{
    private static Dictionary<string, List<Tarjeta>> tarjetasPorCliente = new();
    private static long numeroActual = 1111111111111111;

    [HttpPost("emitir")]
    public ActionResult<TarjetaDto> EmitirTarjeta([FromBody] EmitirTarjetaRequest request)
    {
        var numero = GenerarNumeroTarjeta();
        var tarjeta = new Tarjeta
        {
            Numero = numero,
            ClienteId = request.ClienteId,
            Tipo = request.Tipo,
            FechaEmision = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddYears(5),
            Estado = "Activa",
            LimiteCredito = request.Tipo == "credito" ? 5000 : 0
        };

        if (!tarjetasPorCliente.ContainsKey(request.ClienteId))
            tarjetasPorCliente[request.ClienteId] = new List<Tarjeta>();

        tarjetasPorCliente[request.ClienteId].Add(tarjeta);

        return Ok(new TarjetaDto(numero, request.Tipo, "Activa"));
    }

    [HttpGet("cliente/{clienteId}")]
    public ActionResult<List<TarjetaDto>> ObtenerTarjetas(string clienteId)
    {
        if (!tarjetasPorCliente.ContainsKey(clienteId))
            return Ok(new List<TarjetaDto>());

        return Ok(tarjetasPorCliente[clienteId]
            .Select(t => new TarjetaDto(t.Numero, t.Tipo, t.Estado))
            .ToList());
    }

    private string GenerarNumeroTarjeta()
    {
        numeroActual++;
        return numeroActual.ToString();
    }
}

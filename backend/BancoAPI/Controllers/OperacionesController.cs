using Microsoft.AspNetCore.Mvc;
using BancoAPI.DTOs;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacionesController : ControllerBase
{
    private static Dictionary<string, decimal> saldos = new()
    {
        { "1234567890", 5000m },
        { "0987654321", 3500m },
        { "1122334455", 7200m }
    };

    [HttpPost("deposito")]
    public ActionResult<OperacionResultado> Depositar([FromBody] OperacionRequest request)
    {
        if (!saldos.ContainsKey(request.ClienteId))
            return NotFound(new { mensaje = "Cliente no encontrado" });

        if (request.Monto <= 0)
            return BadRequest(new { mensaje = "Monto debe ser positivo" });

        saldos[request.ClienteId] += request.Monto;

        return Ok(new OperacionResultado(
            true,
            $"Depósito de ${request.Monto} exitoso",
            saldos[request.ClienteId]
        ));
    }

    [HttpPost("retiro")]
    public ActionResult<OperacionResultado> Retirar([FromBody] OperacionRequest request)
    {
        if (!saldos.ContainsKey(request.ClienteId))
            return NotFound(new { mensaje = "Cliente no encontrado" });

        if (request.Monto <= 0)
            return BadRequest(new { mensaje = "Monto debe ser positivo" });

        if (saldos[request.ClienteId] < request.Monto)
            return BadRequest(new { mensaje = "Saldo insuficiente" });

        saldos[request.ClienteId] -= request.Monto;

        return Ok(new OperacionResultado(
            true,
            $"Retiro de ${request.Monto} exitoso",
            saldos[request.ClienteId]
        ));
    }
}

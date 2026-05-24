using Microsoft.AspNetCore.Mvc;
using GymAPI.Models;
using GymAPI.DTOs;

namespace GymAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private static Dictionary<string, decimal> deudas = new()
    {
        { "GYM-001", 45m },
        { "GYM-002", 90m },
        { "GYM-003", 0m }
    };

    private static List<Cuota> pagosConfirmados = new();

    [HttpPost("confirmar")]
    public ActionResult<ConfirmacionResultado> ConfirmarPago([FromBody] ConfirmarPagoRequest request)
    {
        if (!deudas.ContainsKey(request.MiembroId))
            return NotFound(new { mensaje = "Miembro no encontrado" });

        deudas[request.MiembroId] = 0m;

        var cuota = new Cuota
        {
            MiembroId = request.MiembroId,
            Monto = request.Monto,
            Estado = "Pagada",
            FechaPago = DateTime.Now,
            ReferenciaTransaccion = request.ReferenciaTransaccion
        };

        pagosConfirmados.Add(cuota);

        return Ok(new ConfirmacionResultado(
            true,
            $"Pago confirmado por ${request.Monto}"
        ));
    }

    [HttpGet("historial")]
    public ActionResult<List<Cuota>> ObtenerHistorial()
    {
        return Ok(pagosConfirmados);
    }
}

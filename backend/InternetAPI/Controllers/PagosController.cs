using Microsoft.AspNetCore.Mvc;
using InternetAPI.Models;
using InternetAPI.DTOs;

namespace InternetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private static Dictionary<string, decimal> deudas = new()
    {
        { "INET-2024-567", 25m },
        { "INET-2024-568", 50m },
        { "INET-2024-569", 0m }
    };

    private static List<Factura> pagosConfirmados = new();

    [HttpPost("confirmar")]
    public ActionResult<ConfirmacionResultado> ConfirmarPago([FromBody] ConfirmarPagoRequest request)
    {
        if (!deudas.ContainsKey(request.ContratoId))
            return NotFound(new { mensaje = "Contrato no encontrado" });

        deudas[request.ContratoId] = 0m;

        var factura = new Factura
        {
            ContratoId = request.ContratoId,
            Monto = request.Monto,
            Estado = "Pagada",
            FechaPago = DateTime.Now,
            ReferenciaTransaccion = request.ReferenciaTransaccion
        };

        pagosConfirmados.Add(factura);

        return Ok(new ConfirmacionResultado(
            true,
            $"Pago confirmado por ${request.Monto}"
        ));
    }

    [HttpGet("historial")]
    public ActionResult<List<Factura>> ObtenerHistorial()
    {
        return Ok(pagosConfirmados);
    }
}

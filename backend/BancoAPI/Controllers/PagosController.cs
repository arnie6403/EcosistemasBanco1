using Microsoft.AspNetCore.Mvc;
using BancoAPI.Models;
using BancoAPI.DTOs;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private static Dictionary<string, decimal> saldosPago = new()
    {
        { "1111111111111111", 5000m },
        { "1234567890123456", 3500m },
        { "1122334455667788", 7200m }
    };

    private static List<Pago> historialPagos = new();
    private static int referencia = 1000;

    [HttpPost("procesar")]
    public ActionResult<PagoResultado> ProcesarPago([FromBody] ProcesarPagoRequest request)
    {
        // Validar tarjeta
        if (!saldosPago.ContainsKey(request.NumeroTarjeta))
            return BadRequest(new { mensaje = "Tarjeta no válida" });

        if (request.Monto <= 0)
            return BadRequest(new { mensaje = "Monto inválido" });

        if (saldosPago[request.NumeroTarjeta] < request.Monto)
            return BadRequest(new { mensaje = "Fondos insuficientes" });

        // Procesar pago
        saldosPago[request.NumeroTarjeta] -= request.Monto;
        var ref_transaccion = GenerarReferencia();
        var comision = request.Monto * 0.05m; // 5% comisión

        var pago = new Pago
        {
            Referencia = ref_transaccion,
            NumeroTarjeta = MascararTarjeta(request.NumeroTarjeta),
            Monto = request.Monto,
            Comision = comision,
            Fecha = DateTime.Now,
            Estado = "Exitoso"
        };

        historialPagos.Add(pago);

        return Ok(new PagoResultado(
            true,
            ref_transaccion,
            $"Pago de ${request.Monto} procesado exitosamente",
            comision
        ));
    }

    [HttpGet("historial")]
    public ActionResult<List<Pago>> ObtenerHistorial()
    {
        return Ok(historialPagos);
    }

    private string GenerarReferencia()
    {
        referencia++;
        return $"PAG-{referencia}";
    }

    private string MascararTarjeta(string tarjeta)
    {
        return "****-****-****-" + tarjeta.Substring(tarjeta.Length - 4);
    }
}

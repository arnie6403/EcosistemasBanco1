using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Models;
using UniversidadAPI.DTOs;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private static Dictionary<string, decimal> deudas = new()
    {
        { "UNI-2024-001", 350m },
        { "UNI-2024-002", 500m },
        { "UNI-2024-003", 0m }
    };

    private static List<Matricula> pagosConfirmados = new();

    [HttpPost("confirmar")]
    public ActionResult<ConfirmacionResultado> ConfirmarPago([FromBody] ConfirmarPagoRequest request)
    {
        if (!deudas.ContainsKey(request.EstudianteId))
            return NotFound(new { mensaje = "Estudiante no encontrado" });

        deudas[request.EstudianteId] = 0m;

        var matricula = new Matricula
        {
            EstudianteId = request.EstudianteId,
            Monto = request.Monto,
            Estado = "Pagada",
            FechaPago = DateTime.Now,
            ReferenciaTransaccion = request.ReferenciaTransaccion
        };

        pagosConfirmados.Add(matricula);

        return Ok(new ConfirmacionResultado(
            true,
            $"Pago confirmado por ${request.Monto}"
        ));
    }

    [HttpGet("historial")]
    public ActionResult<List<Matricula>> ObtenerHistorial()
    {
        return Ok(pagosConfirmados);
    }
}

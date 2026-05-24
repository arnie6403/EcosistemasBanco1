using Microsoft.AspNetCore.Mvc;
using InternetAPI.Models;
using InternetAPI.DTOs;

namespace InternetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratosController : ControllerBase
{
    private static Dictionary<string, Contrato> contratos = new()
    {
        { "INET-2024-567", new Contrato { Id = "INET-2024-567", ClienteId = "CLI-001", Nombre = "Hogar García", DeudaPendiente = 25m, FechaContratacion = DateTime.Now } },
        { "INET-2024-568", new Contrato { Id = "INET-2024-568", ClienteId = "CLI-002", Nombre = "Hogar López", DeudaPendiente = 50m, FechaContratacion = DateTime.Now } },
        { "INET-2024-569", new Contrato { Id = "INET-2024-569", ClienteId = "CLI-003", Nombre = "Hogar Martín", DeudaPendiente = 0m, FechaContratacion = DateTime.Now } }
    };

    [HttpGet("{contratoId}")]
    public ActionResult<ContratoDto> GetContrato(string contratoId)
    {
        if (contratos.TryGetValue(contratoId, out var contrato))
        {
            return Ok(new ContratoDto(
                contrato.Id,
                contrato.Nombre,
                contrato.DeudaPendiente
            ));
        }
        return NotFound(new { mensaje = "Contrato no encontrado" });
    }

    [HttpPost("crear")]
    public ActionResult<ContratoDto> CrearContrato([FromBody] CrearContratoRequest request)
    {
        if (contratos.ContainsKey(request.ContratoId))
            return BadRequest(new { mensaje = "Contrato ya existe" });

        var contrato = new Contrato
        {
            Id = request.ContratoId,
            ClienteId = request.ClienteId,
            Nombre = request.Nombre,
            Velocidad = request.Velocidad,
            DeudaPendiente = 25m, // Cuota inicial
            FechaContratacion = DateTime.Now
        };

        contratos[request.ContratoId] = contrato;

        return CreatedAtAction(nameof(GetContrato), new { contratoId = contrato.Id },
            new ContratoDto(contrato.Id, contrato.Nombre, contrato.DeudaPendiente));
    }

    [HttpGet]
    public ActionResult<List<ContratoDto>> GetTodos()
    {
        return Ok(contratos.Values.Select(c => new ContratoDto(c.Id, c.Nombre, c.DeudaPendiente)).ToList());
    }
}

using Microsoft.AspNetCore.Mvc;
using GymAPI.Models;
using GymAPI.DTOs;

namespace GymAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiembrosController : ControllerBase
{
    private static Dictionary<string, Miembro> miembros = new()
    {
        { "GYM-001", new Miembro { Id = "GYM-001", Nombre = "Ana García", DeudaPendiente = 45m, FechaRegistro = DateTime.Now } },
        { "GYM-002", new Miembro { Id = "GYM-002", Nombre = "Pedro López", DeudaPendiente = 90m, FechaRegistro = DateTime.Now } },
        { "GYM-003", new Miembro { Id = "GYM-003", Nombre = "Laura Martín", DeudaPendiente = 0m, FechaRegistro = DateTime.Now } }
    };

    [HttpGet("{miembroId}")]
    public ActionResult<MiembroDto> GetMiembro(string miembroId)
    {
        if (miembros.TryGetValue(miembroId, out var miembro))
        {
            return Ok(new MiembroDto(
                miembro.Id,
                miembro.Nombre,
                miembro.DeudaPendiente
            ));
        }
        return NotFound(new { mensaje = "Miembro no encontrado" });
    }

    [HttpPost("crear")]
    public ActionResult<MiembroDto> CrearMiembro([FromBody] CrearMiembroRequest request)
    {
        if (miembros.ContainsKey(request.MiembroId))
            return BadRequest(new { mensaje = "Miembro ya existe" });

        var miembro = new Miembro
        {
            Id = request.MiembroId,
            Nombre = request.Nombre,
            Email = request.Email,
            Telefono = request.Telefono,
            DeudaPendiente = 45m, // Cuota inicial
            FechaRegistro = DateTime.Now,
            TipoMembresia = request.TipoMembresia
        };

        miembros[request.MiembroId] = miembro;

        return CreatedAtAction(nameof(GetMiembro), new { miembroId = miembro.Id },
            new MiembroDto(miembro.Id, miembro.Nombre, miembro.DeudaPendiente));
    }

    [HttpGet]
    public ActionResult<List<MiembroDto>> GetTodos()
    {
        return Ok(miembros.Values.Select(m => new MiembroDto(m.Id, m.Nombre, m.DeudaPendiente)).ToList());
    }
}

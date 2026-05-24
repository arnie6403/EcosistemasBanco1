using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Models;
using UniversidadAPI.DTOs;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private static Dictionary<string, Estudiante> estudiantes = new()
    {
        { "UNI-2024-001", new Estudiante { Id = "UNI-2024-001", Nombre = "Roberto Sánchez", DeudaPendiente = 350m, FechaRegistro = DateTime.Now, Carrera = "Ingeniería", Semestre = 5 } },
        { "UNI-2024-002", new Estudiante { Id = "UNI-2024-002", Nombre = "Sofia Rivera", DeudaPendiente = 500m, FechaRegistro = DateTime.Now, Carrera = "Administración", Semestre = 3 } },
        { "UNI-2024-003", new Estudiante { Id = "UNI-2024-003", Nombre = "Diego Fernández", DeudaPendiente = 0m, FechaRegistro = DateTime.Now, Carrera = "Derecho", Semestre = 6 } }
    };

    [HttpGet("{estudianteId}")]
    public ActionResult<EstudianteDto> GetEstudiante(string estudianteId)
    {
        if (estudiantes.TryGetValue(estudianteId, out var estudiante))
        {
            return Ok(new EstudianteDto(
                estudiante.Id,
                estudiante.Nombre,
                estudiante.DeudaPendiente
            ));
        }
        return NotFound(new { mensaje = "Estudiante no encontrado" });
    }

    [HttpPost("crear")]
    public ActionResult<EstudianteDto> CrearEstudiante([FromBody] CrearEstudianteRequest request)
    {
        if (estudiantes.ContainsKey(request.EstudianteId))
            return BadRequest(new { mensaje = "Estudiante ya existe" });

        var estudiante = new Estudiante
        {
            Id = request.EstudianteId,
            Nombre = request.Nombre,
            Email = request.Email,
            Carrera = request.Carrera,
            DeudaPendiente = 350m, // Matrícula inicial
            FechaRegistro = DateTime.Now,
            Semestre = request.Semestre
        };

        estudiantes[request.EstudianteId] = estudiante;

        return CreatedAtAction(nameof(GetEstudiante), new { estudianteId = estudiante.Id },
            new EstudianteDto(estudiante.Id, estudiante.Nombre, estudiante.DeudaPendiente));
    }

    [HttpGet]
    public ActionResult<List<EstudianteDto>> GetTodos()
    {
        return Ok(estudiantes.Values.Select(e => new EstudianteDto(e.Id, e.Nombre, e.DeudaPendiente)).ToList());
    }
}

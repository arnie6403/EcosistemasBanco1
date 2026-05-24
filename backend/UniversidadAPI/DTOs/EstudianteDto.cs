namespace UniversidadAPI.DTOs;

public record EstudianteDto(
    string Id,
    string Nombre,
    decimal DeudaPendiente
);

public record CrearEstudianteRequest(
    string EstudianteId,
    string Nombre,
    string Email,
    string Carrera,
    int Semestre = 1
);

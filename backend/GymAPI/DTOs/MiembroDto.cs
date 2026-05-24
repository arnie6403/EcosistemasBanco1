namespace GymAPI.DTOs;

public record MiembroDto(
    string Id,
    string Nombre,
    decimal DeudaPendiente
);

public record CrearMiembroRequest(
    string MiembroId,
    string Nombre,
    string Email,
    string Telefono,
    string TipoMembresia = "Básica"
);

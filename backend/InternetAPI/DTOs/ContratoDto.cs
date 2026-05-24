namespace InternetAPI.DTOs;

public record ContratoDto(
    string Id,
    string Nombre,
    decimal DeudaPendiente
);

public record CrearContratoRequest(
    string ContratoId,
    string ClienteId,
    string Nombre,
    string Velocidad = "25Mbps"
);

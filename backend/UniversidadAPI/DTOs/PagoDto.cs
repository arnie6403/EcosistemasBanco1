namespace UniversidadAPI.DTOs;

public record ConfirmarPagoRequest(
    string EstudianteId,
    decimal Monto,
    string ReferenciaTransaccion
);

public record ConfirmacionResultado(
    bool Exito,
    string Mensaje
);

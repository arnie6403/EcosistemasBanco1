namespace GymAPI.DTOs;

public record ConfirmarPagoRequest(
    string MiembroId,
    decimal Monto,
    string ReferenciaTransaccion
);

public record ConfirmacionResultado(
    bool Exito,
    string Mensaje
);

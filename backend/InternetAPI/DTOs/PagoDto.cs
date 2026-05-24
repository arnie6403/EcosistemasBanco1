namespace InternetAPI.DTOs;

public record ConfirmarPagoRequest(
    string ContratoId,
    decimal Monto,
    string ReferenciaTransaccion
);

public record ConfirmacionResultado(
    bool Exito,
    string Mensaje
);

namespace BancoAPI.DTOs;

public record ProcesarPagoRequest(
    string NumeroTarjeta,
    string Cvv,
    decimal Monto
);

public record PagoResultado(
    bool Exito,
    string Referencia,
    string Mensaje,
    decimal MontoComision
);

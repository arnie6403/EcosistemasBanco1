namespace BancoAPI.DTOs;

public record OperacionRequest(
    string ClienteId,
    decimal Monto
);

public record OperacionResultado(
    bool Exito,
    string Mensaje,
    decimal NuevoSaldo
);

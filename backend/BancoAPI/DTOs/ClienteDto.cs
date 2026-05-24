namespace BancoAPI.DTOs;

public record ClienteDto(
    string Id,
    string Nombre,
    decimal Saldo
);

public record CrearClienteRequest(
    string ClienteId,
    string Nombre,
    decimal SaldoInicial
);

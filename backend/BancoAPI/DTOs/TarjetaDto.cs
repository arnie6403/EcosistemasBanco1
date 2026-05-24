namespace BancoAPI.DTOs;

public record TarjetaDto(
    string Numero,
    string Tipo,
    string Estado
);

public record EmitirTarjetaRequest(
    string ClienteId,
    string Tipo
);

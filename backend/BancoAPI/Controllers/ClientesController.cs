using Microsoft.AspNetCore.Mvc;
using BancoAPI.Models;
using BancoAPI.DTOs;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    // Simulación de base de datos en memoria
    private static Dictionary<string, Cliente> clientes = new()
    {
        { "1234567890", new Cliente { Id = "1234567890", Nombre = "Juan García", Saldo = 5000m, FechaRegistro = DateTime.Now } },
        { "0987654321", new Cliente { Id = "0987654321", Nombre = "María López", Saldo = 3500m, FechaRegistro = DateTime.Now } },
        { "1122334455", new Cliente { Id = "1122334455", Nombre = "Carlos Pérez", Saldo = 7200m, FechaRegistro = DateTime.Now } }
    };

    [HttpGet("{clienteId}")]
    public ActionResult<ClienteDto> GetCliente(string clienteId)
    {
        if (clientes.TryGetValue(clienteId, out var cliente))
        {
            return Ok(new ClienteDto(
                cliente.Id,
                cliente.Nombre,
                cliente.Saldo
            ));
        }
        return NotFound(new { mensaje = "Cliente no encontrado" });
    }

    [HttpPost("crear")]
    public ActionResult<ClienteDto> CrearCliente([FromBody] CrearClienteRequest request)
    {
        if (clientes.ContainsKey(request.ClienteId))
            return BadRequest(new { mensaje = "Cliente ya existe" });

        var cliente = new Cliente
        {
            Id = request.ClienteId,
            Nombre = request.Nombre,
            Saldo = request.SaldoInicial,
            FechaRegistro = DateTime.Now,
            Estado = "Activo"
        };

        clientes[request.ClienteId] = cliente;

        return CreatedAtAction(nameof(GetCliente), new { clienteId = cliente.Id },
            new ClienteDto(cliente.Id, cliente.Nombre, cliente.Saldo));
    }

    [HttpGet]
    public ActionResult<List<ClienteDto>> GetTodos()
    {
        return Ok(clientes.Values.Select(c => new ClienteDto(c.Id, c.Nombre, c.Saldo)).ToList());
    }
}

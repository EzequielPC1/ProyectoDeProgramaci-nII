using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 

[ApiController]
[Route("api/clientes")]
[Authorize(Roles = "Administrador")] 
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // Obtener todos los clientes
    [HttpGet]
    public ActionResult<List<Cliente>> GetAllClientes()
    {
        return Ok(_clienteService.GetAll());
    }

    // Obtener un cliente por ID
    [HttpGet("{id}")]
    public ActionResult<Cliente> GetById(int id)
    {
        var cliente = _clienteService.GetById(id);
        if (cliente == null)
        {
            return NotFound("Cliente no encontrado");
        }
        return Ok(cliente);
    }

    // Crear un nuevo cliente
    [HttpPost]
    public ActionResult<Cliente> NuevoCliente(ClienteDTO cliente)
    {
        Cliente _cliente = _clienteService.Create(cliente);
        return CreatedAtAction(nameof(GetById), new { id = _cliente.Id }, _cliente);
    }

    // Eliminar un cliente por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var cliente = _clienteService.GetById(id);
        if (cliente == null)
        {
            return NotFound("Cliente no encontrado");
        }

        _clienteService.Delete(id);
        return NoContent();
    }

    // Actualizar un cliente
    [HttpPut("{id}")]
    public ActionResult<Cliente> UpdateCliente(int id, Cliente updatedCliente)
    {
        if (id != updatedCliente.Id)
        {
            return BadRequest("El ID del cliente en la URL no coincide con el ID del cliente en el cuerpo de la solicitud.");
        }

        var cliente = _clienteService.Update(id, updatedCliente);

        if (cliente is null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }
}

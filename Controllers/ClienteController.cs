using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clientes")]
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
    public ActionResult<Cliente> NuevoCliente(Cliente cliente)
    {
        // Llamar al método Create del servicio de cliente para dar de alta el nuevo cliente
        cliente = _clienteService.Create(cliente);
        // Devolver el resultado de llamar al método GetById pasando como parámetro el ID del nuevo cliente
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
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
        return NoContent(); // Retorna un 204 No Content
    }

    // Actualizar un cliente
    [HttpPut("{id}")]
    public ActionResult<Cliente> UpdateCliente(int id, Cliente updatedCliente)
    {
        // Asegurarse de que el ID del cliente en la solicitud coincida con el ID del parámetro
        if (id != updatedCliente.Id)
        {
            return BadRequest("El ID del cliente en la URL no coincide con el ID del cliente en el cuerpo de la solicitud.");
        }

        var cliente = _clienteService.Update(id, updatedCliente);

        if (cliente is null)
        {
            return NotFound(); // Si no se encontró el cliente, retorna 404 Not Found
        }
        return Ok(cliente); // Retorna el recurso actualizado
    }
}

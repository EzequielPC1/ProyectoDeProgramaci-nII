using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pedidos")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    // Obtener todos los pedidos
    [HttpGet]
    public ActionResult<List<Pedido>> GetAllPedidos()
    {
        return Ok(_pedidoService.GetAll());
    }

    // Obtener un pedido por ID
    [HttpGet("{id}")]
    public ActionResult<Pedido> GetPedidoById(int id)
    {
        var pedido = _pedidoService.GetById(id);
        if (pedido == null)
        {
            return NotFound("Pedido no encontrado");
        }
        return Ok(pedido);
    }

    [HttpGet("estado/{estado}")]
    public ActionResult<List<Pedido>> GetPedidoByEstado(EstadoPedido estadoPedido)
    {
        var pedidos = _pedidoService.GetByEstado(estadoPedido);
        if (pedidos == null)
        {
            return NotFound("No se encontraron pedidos con el estado especificado");
        }
        return Ok(pedidos);
    }

    [HttpGet("fecha/{fecha}")]
    public ActionResult<List<Pedido>> GetPedidoByFecha(DateTime fecha)
    {
        var pedidos = _pedidoService.GetByFecha(fecha);
        if (pedidos == null)
        {
            return NotFound("No se encontraron pedidos con el estado especificado");
        }
        return Ok(pedidos);
    }

    [HttpGet("cliente/{cliente}")]
    public ActionResult<List<Pedido>> GetPedidoByCliente(Cliente cliente)
    {
        var pedidos = _pedidoService.GetByCliente(cliente);
        if (pedidos == null)
        {
            return NotFound("No se encontraron pedidos con el estado especificado");
        }
        return Ok(pedidos);
    }

    // Crear un nuevo pedido
    [HttpPost]
    public ActionResult<Pedido> NuevoPedido(Pedido pedido)
    {
        // Llamar al método Create del servicio de pedidos para dar de alta el nuevo pedido
        pedido = _pedidoService.Create(pedido);
        // Devolver el resultado de llamar al método GetById pasando como parámetro el ID del nuevo pedido
        return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedido);
    }

    // Eliminar un pedido por ID
    [HttpDelete("{id}")]
    public ActionResult DeletePedido(int id)
    {
        var pedido = _pedidoService.GetById(id);
        if (pedido == null)
        {
            return NotFound("Pedido no encontrado");
        }

        _pedidoService.Delete(id);
        return NoContent(); // Retorna un 204 No Content
    }

    // Actualizar un pedido
    [HttpPut("{id}")]
    public ActionResult<Pedido> UpdatePedido(int id, Pedido updatedPedido)
    {
        // Asegurarse de que el ID del Pedido en la solicitud coincida con el ID del parámetro
        if (id != updatedPedido.Id)
        {
            return BadRequest("El ID del Pedido en la URL no coincide con el ID en el cuerpo de la solicitud.");
        }

        var pedido = _pedidoService.Update(id, updatedPedido);

        if (pedido is null)
        {
            return NotFound(); // Si no se encontró el Pedido, retorna 404 Not Found
        }
        return Ok(pedido); // Retorna el recurso actualizado
    }
}

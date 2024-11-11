using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pedidos")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    private readonly IClienteService _clienteService;
    private readonly IItemService _itemService;


    public PedidoController(IPedidoService pedidoService, IClienteService clienteService, IItemService itemService) 
    {
        _pedidoService = pedidoService;
        _clienteService = clienteService;
        _itemService = itemService;
    }

    // Obtener todos los pedidos
    [HttpGet]
    public ActionResult<List<Pedido>> GetAllPedidos()
    {
        try
      {
        return Ok(_pedidoService.GetAll());
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
      }
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
//     [HttpGet("estado/{estado}")]
// public ActionResult<List<Pedido>> GetPedidoByEstado(string estado)
// {
//     if (!Enum.TryParse<EstadoPedido>(estado, true, out var estadoPedido))
//     {
//         return BadRequest($"El estado '{estado}' no es válido.");
//     }

//     var pedidos = _pedidoService.GetByEstado(estadoPedido);
//     if (pedidos == null || pedidos.Count == 0)
//     {
//         return NotFound($"No se encontraron pedidos con el estado '{estadoPedido}'");
//     }
//     return Ok(pedidos);
// }


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

    [HttpGet("cliente/{clienteId}")]
public ActionResult<List<Pedido>> GetPedidoByCliente(int clienteId)
{
    var cliente = _clienteService.GetById(clienteId);  // Obtiene el cliente por ID
    if (cliente == null)
    {
        return NotFound($"Cliente con ID {clienteId} no encontrado.");
    }
    var pedidos = _pedidoService.GetByCliente(cliente); // Llama al servicio con el cliente obtenido
    if (pedidos == null || pedidos.Count == 0)
    {
        return NotFound("No se encontraron pedidos para este cliente.");
    }
    return Ok(pedidos); // Devuelve los pedidos del cliente
}


    // Crear un nuevo pedido
[HttpPost]
public ActionResult<Pedido> NuevoPedido(PedidoDTO pedidoDTO)
{
    // Validar el modelo de datos
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Obtener el cliente por ID
    var cliente = _clienteService.GetById(pedidoDTO.ClienteId);
    if (cliente == null)
        return NotFound($"Cliente con ID {pedidoDTO.ClienteId} no encontrado.");

    // Crear el pedido
    var pedido = new Pedido
    {
        Cliente = cliente,
        Fecha = pedidoDTO.Fecha,
        EstadoPedido = pedidoDTO.EstadoPedido,
        Items = pedidoDTO.ItemIds?.Select(id => _itemService.GetById(id)).Where(item => item != null).ToList() ?? new List<Item>()
    };

    // Crear el pedido en el servicio
    var nuevoPedido = _pedidoService.Create(pedidoDTO);

    // Retornar el pedido creado
    return CreatedAtAction(nameof(GetPedidoById), new { id = nuevoPedido.Id }, nuevoPedido);
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

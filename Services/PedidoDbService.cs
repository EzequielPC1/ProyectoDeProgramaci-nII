using Microsoft.EntityFrameworkCore;

public class PedidoDbService : IPedidoService
{
    private readonly DeliveryContext _context;

    public PedidoDbService(DeliveryContext context)
    {
        _context = context;
    }

    public IEnumerable<Pedido> GetAll()
    {
        return _context.Pedidos.Include(el => el.Cliente).Include(los => los.Items);
        
    }


    public Pedido GetById(int id)
    {
        return _context.Pedidos.Find(id);
    }
    public List<Pedido> GetByFecha(DateTime fecha)
    {
        return _context.Pedidos.Include(el => el.Cliente).Include(los => los.Items)
                       .Where(p => p.Fecha.Date == fecha.Date) 
                       .ToList();
    }
    public List<Pedido> GetByEstado(EstadoPedido estadoPedido)
{
    return _context.Pedidos.Include(el => el.Cliente).Include(los => los.Items)
                           .Where(p => p.EstadoPedido == estadoPedido)
                           .ToList();
}


    public List<Pedido> GetByCliente(Cliente cliente)
{
    return _context.Pedidos.Include(el => el.Cliente).Include(los => los.Items)
                           .Where(p => p.Cliente.Id == cliente.Id) // Usar el Id del cliente
                           .ToList();
}

    public Pedido Create(PedidoDTO pedidoDTO)
{
    // Crear el nuevo pedido
    var nuevoPedido = new Pedido
    {
        Fecha = pedidoDTO.Fecha,
        EstadoPedido = pedidoDTO.EstadoPedido,
        Cliente = _context.Clientes.Find(pedidoDTO.ClienteId), // Busca al cliente por su ID
        Items = new List<Item>()
    };

    // Agregar los items al pedido a partir de los IDs proporcionados en el DTO
    foreach (var itemId in pedidoDTO.ItemIds ?? new List<int>())
    {
        var item = _context.Items.Find(itemId); // Busca el item por ID
        if (item != null)
        {
            nuevoPedido.Items.Add(item); // Añadir el item al pedido
        }
    }

    // Añadir el nuevo pedido al contexto
    _context.Pedidos.Add(nuevoPedido);

    // Guardar los cambios en la base de datos
    _context.SaveChanges();

    // Retornar el nuevo pedido creado
    return nuevoPedido;
}

    // Eliminar un pedido
    public void Delete(int id)
    {
        var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido == null)
        {
            throw new Exception($"Pedido con ID {id} no encontrado.");
        }

        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
    }
    public Pedido Update(int id, Pedido pedido)
    {
        throw new Exception("");
    }
}

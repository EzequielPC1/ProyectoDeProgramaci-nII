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
        return _context.Pedidos;
    }

    public Pedido GetById(int id)
    {
        throw new Exception(""); 
    }
    public Pedido GetByFecha(DateTime fecha)
    {
                throw new Exception(""); 
    }

    public Pedido GetByEstado(EstadoPedido estadoPedido)
    {
                throw new Exception(""); 
    }

    public Pedido GetByCliente(Cliente cliente)
    {
        throw new Exception(""); 
    }
    public Pedido Create(Pedido pedido)
    {
        throw new Exception(""); 
    }
    public void Delete(int id)
    {
        throw new Exception(""); 
    }
    public Pedido Update(int id, Pedido pedido)
    {
        throw new Exception("");
    }
}

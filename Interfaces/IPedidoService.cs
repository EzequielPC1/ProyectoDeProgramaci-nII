public interface IPedidoService
{
    public IEnumerable<Pedido> GetAll();
    public Pedido GetById(int id);
    public Pedido GetByEstado(EstadoPedido estadoPedido);
    public Pedido GetByFecha(DateTime fecha);
    public Pedido GetByCliente(Cliente cliente);

    public Pedido Create(Pedido pedido);
    public void Delete(int id);
    public Pedido Update(int id, Pedido pedido);
}
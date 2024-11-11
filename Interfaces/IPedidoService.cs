public interface IPedidoService
{
    public IEnumerable<Pedido> GetAll();
    public Pedido GetById(int id);
    List<Pedido> GetByEstado(EstadoPedido estadoPedido);
    // public Pedido GetByFecha(DateTime fecha);
    List<Pedido> GetByCliente(Cliente cliente);

    public Pedido Create(PedidoDTO pedido);
    public void Delete(int id);
    public Pedido Update(int id, Pedido pedido);
    List<Pedido> GetByFecha(DateTime fecha);
}
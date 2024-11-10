public class Pedido
{

    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public required Cliente Cliente { get; set; }
    public required List<Item> Items { get; set; }
    public EstadoPedido EstadoPedido { get; set; }
}

public enum EstadoPedido
{
    Ingresado,
    EnProceso,
    Enviado,
    Entregado
}

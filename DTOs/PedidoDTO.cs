using System.ComponentModel.DataAnnotations;

public class PedidoDTO
{
    // Se requiere la fecha del pedido
    [Required(ErrorMessage = "El campo Fecha es requerido.")]
    public DateTime Fecha { get; set; }

    // Se requiere el ID del cliente que realiza el pedido
    [Required(ErrorMessage = "El ID del cliente es requerido.")]
    public int ClienteId { get; set; }  

    // Lista de IDs de items incluidos en el pedido, opcional
    public List<int>? ItemIds { get; set; }  

    // Estado del pedido; debe coincidir con los valores definidos en EstadoPedido
    [Required(ErrorMessage = "El estado es requerido.")]
    [EnumDataType(typeof(EstadoPedido), ErrorMessage = "El estado especificado no es válido.")]
    public EstadoPedido EstadoPedido { get; set; }

    public string EstadoPedidoDescripcion => EstadoPedido.ToString();
}

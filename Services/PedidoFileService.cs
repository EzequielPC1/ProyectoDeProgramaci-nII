using System.Text.Json;

public class PedidoFileService : IPedidoService
{    
    private readonly string _filePath = "Data/Pedidos.json";
    private readonly IFileStorageService _fileStorageService;

    public PedidoFileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    public IEnumerable<Pedido> GetAll()
    {
        var json = _fileStorageService.Read(_filePath);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public Pedido GetById(int id)
{
    var json = _fileStorageService.Read(_filePath);
    var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();

    var pedido = pedidos.Find(p => p.Id == id);
    if (pedido == null)
    {
        throw new KeyNotFoundException($"No se encontró un pedido con ID {id}.");
    }

    return pedido;
}

    public Pedido GetByEstado(EstadoPedido estadoPedido)
{
    var json = _fileStorageService.Read(_filePath);
    var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();

    var pedido = pedidos.Find(p => p.EstadoPedido == estadoPedido);
    
    if (pedido == null)
    {
        throw new KeyNotFoundException($"No se encontró un pedido con estado {estadoPedido}.");
    }

    return pedido;
}

    public Pedido GetByFecha(DateTime fecha)
{
    var json = _fileStorageService.Read(_filePath);
    var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();

    var pedido = pedidos.Find(p => p.Fecha.Date == fecha.Date);
    
    if (pedido == null)
    {
        throw new KeyNotFoundException($"No se encontró un pedido con fecha {fecha.ToShortDateString()}.");
    }

    return pedido;
}

    public Pedido GetByCliente(Cliente cliente)
{
    var json = _fileStorageService.Read(_filePath);
    var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();

    var pedido = pedidos.Find(p => p.Cliente == cliente); 
    
    if (pedido == null)
    {
        throw new KeyNotFoundException($"No se encontró un pedido para el cliente con ID {cliente.Id}.");
    }

    return pedido;
}


    public Pedido Create(Pedido pedido)
    {
        var json = _fileStorageService.Read(_filePath);
        var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        pedidos.Add(pedido);
        json = JsonSerializer.Serialize(pedido);
        _fileStorageService.Write(_filePath, json);
        return pedido;
    }

    public void Delete(int id)
    {
        var json = _fileStorageService.Read(_filePath);
        var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        var pedido = pedidos.Find(c => c.Id == id);
        if (pedido is not null)
        {
            pedidos.Remove(pedido);
            json = JsonSerializer.Serialize(pedidos);
            _fileStorageService.Write(_filePath, json);
        }
    }

    public Pedido Update(int id, Pedido pedido)
{
    var json = _fileStorageService.Read(_filePath);
    var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();

    var pedidoIndex = pedidos.FindIndex(c => c.Id == id);
    if (pedidoIndex >= 0)
    {
        pedidos[pedidoIndex] = pedido; // Actualiza el pedido
        json = JsonSerializer.Serialize(pedidos);
        _fileStorageService.Write(_filePath, json);
        return pedido; // Devuelve el pedido actualizado
    }
    
    throw new KeyNotFoundException($"No se encontró un pedido con ID {id}."); // Lanza una excepción si no se encontró
}

}
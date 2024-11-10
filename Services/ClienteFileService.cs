using System.Text.Json;

public class ClienteFileService : IClienteService
{
    private readonly string _filePath = "Data/Clientes.json";
    private readonly IFileStorageService _fileStorageService;

    public ClienteFileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public IEnumerable<Cliente> GetAll()
    {
        var json = _fileStorageService.Read(_filePath);
        return JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
    }

    public Cliente GetById(int id)
    {
        var json = _fileStorageService.Read(_filePath);
        var clientes = JsonSerializer.Deserialize<List<Cliente>>(json);
        if (clientes is null) return null;
        return clientes.Find(c => c.Id == id);
    }

    public Cliente Create(Cliente cliente)
    {
        var json = _fileStorageService.Read(_filePath);
        var clientes = JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        clientes.Add(cliente);
        json = JsonSerializer.Serialize(clientes);
        _fileStorageService.Write(_filePath, json);
        return cliente;
    }

    public void Delete(int id)
    {
        var json = _fileStorageService.Read(_filePath);
        var clientes = JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        var cliente = clientes.Find(c => c.Id == id);
        if (cliente is not null)
        {
            clientes.Remove(cliente);
            json = JsonSerializer.Serialize(clientes);
            _fileStorageService.Write(_filePath, json);
        }
    }

    public Cliente Update(int id, Cliente cliente)
    {
        var json = _fileStorageService.Read(_filePath);
        var clientes = JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        var clienteIndex = clientes.FindIndex(c => c.Id == id);
        if (clienteIndex >= 0)
        {
            clientes[clienteIndex] = cliente;
            json = JsonSerializer.Serialize(clientes);
            _fileStorageService.Write(_filePath, json);
            return cliente;
        }
        return null;
    }
}

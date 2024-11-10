using System.Text.Json;

public class ItemFileService : IItemService
{
    private readonly string _filePath = "Data/Items.json";
    private readonly IFileStorageService _fileStorageService;

    public ItemFileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public IEnumerable<Item> GetAll()
    {
        var json = _fileStorageService.Read(_filePath);
        return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
    }
    public Item GetById(int id)
    {
        var json = _fileStorageService.Read(_filePath);
        var items = JsonSerializer.Deserialize<List<Item>>(json);
        if (items is null)
        {
            throw new InvalidOperationException("No se pudieron cargar los items.");
        }

        var item = items.Find(c => c.Id == id);
        if (item == null)
        {
            throw new KeyNotFoundException($"No se encontró un item con ID {id}.");
        }

        return item;
    }


    public Item Create(Item item)
    {
        var json = _fileStorageService.Read(_filePath);
        var items = JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        items.Add(item);
        json = JsonSerializer.Serialize(items);
        _fileStorageService.Write(_filePath, json);
        return item;
    }

    public void Delete(int id)
    {
        var json = _fileStorageService.Read(_filePath);
        var items = JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        var item = items.Find(c => c.Id == id);
        if (item is not null)
        {
            items.Remove(item);
            json = JsonSerializer.Serialize(items);
            _fileStorageService.Write(_filePath, json);
        }
    }

    public Item Update(int id, Item item)
    {
        var json = _fileStorageService.Read(_filePath);
        var items = JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        var itemIndex = items.FindIndex(c => c.Id == id);
        if (itemIndex >= 0)
        {
            items[itemIndex] = item;
            json = JsonSerializer.Serialize(items);
            _fileStorageService.Write(_filePath, json);
            return item;
        }
        throw new KeyNotFoundException($"No se encontró un item con ID {id}.");
    }
}
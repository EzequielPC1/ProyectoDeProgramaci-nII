using Microsoft.EntityFrameworkCore;

public class ItemDbService : IItemService
{
    private readonly DeliveryContext _context;

    public ItemDbService(DeliveryContext context)
    {
        _context = context;
    }

    public IEnumerable<Item> GetAll()
    {
        return _context.Items;
    }
    public Item GetById(int id)
    {
        return _context.Items.Find(id);
    }
    public Item Create(ItemDTO item)
    {
        Item i = new()
        {
            Nombre = item.Nombre,
            Descripción = item.Descripción,
            Precio = item.Precio
        };
        
        _context.Items.Add(i);
        _context.SaveChanges();
        return i;
    }
    public void Delete(int id)
    {
        var item = _context.Items.Find(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }
    }
    public Item Update(int id, Item item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.SaveChanges();
        return item;
    }
}
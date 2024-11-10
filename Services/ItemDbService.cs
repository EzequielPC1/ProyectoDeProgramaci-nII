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
    public Item Create(Item item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
        return item;
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
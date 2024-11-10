public interface IItemService
{
    public IEnumerable<Item> GetAll();
    public Item GetById(int id);
    public Item Create(Item item);
    public void Delete(int id);
    public Item Update(int id, Item item);
}
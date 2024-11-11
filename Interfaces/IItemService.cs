public interface IItemService
{
    public IEnumerable<Item> GetAll();
    public Item GetById(int id);
    public Item Create(ItemDTO item);
    public void Delete(int id);
    public Item Update(int id, Item item);
}
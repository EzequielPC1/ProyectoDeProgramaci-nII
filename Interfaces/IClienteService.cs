public interface IClienteService
{
    public IEnumerable<Cliente> GetAll();
    public Cliente GetById(int id);
    public Cliente Create(Cliente cliente);
    public void Delete(int id);
    public Cliente Update(int id, Cliente cliente);
}   
using Microsoft.EntityFrameworkCore;

public class ClienteDbService : IClienteService
{
    private readonly DeliveryContext _context;

    public ClienteDbService(DeliveryContext context)
    {
        _context = context;
    }

    public IEnumerable<Cliente> GetAll()
    {
        return _context.Clientes;
    }
    public Cliente GetById(int id)
    {
        return _context.Clientes.Find(id);
    }
    public Cliente Create(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return cliente;
    }
    public void Delete(int id)
    {
        var cliente = _context.Clientes.Find(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }
    }
    public Cliente Update(int id, Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
        return cliente;
    }
}

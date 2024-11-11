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
    public Cliente Create(ClienteDTO cliente)
    {
        Cliente c = new()
        {
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Direccion = cliente.Direccion,
            Telefono = cliente.Telefono
        };

        _context.Clientes.Add(c);
        _context.SaveChanges();
        return c;
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

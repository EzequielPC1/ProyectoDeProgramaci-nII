public class Cliente
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }

    public Cliente()
    {
    }

    public Cliente(int id, string nombre, string apellido, string direccion, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        Telefono = telefono;
    }

    public override string ToString()
    {
        return $"{Nombre} {Apellido}, Dirección: {Direccion}, Teléfono: {Telefono}";
    }
}
using System.Text.Json.Serialization;

public class Item
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public string? Descripción { get; set; }
    public decimal Precio { get; set; }  
}

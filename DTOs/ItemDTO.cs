using System.ComponentModel.DataAnnotations;


public class ItemDTO {
  [Required( ErrorMessage = "El campo Nombre es requerido.")]
  public string? Nombre { get; set; }
  [Required( ErrorMessage = "El campo Descripcion es requerido.")]
  public string? Descripción { get; set; }
  [Required( ErrorMessage = "El campo Precio es requerido.")]
  public decimal Precio {get; set;}
}

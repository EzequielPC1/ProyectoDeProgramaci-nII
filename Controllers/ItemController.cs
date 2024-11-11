using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/items")]

public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // Obtener todos los items
    [HttpGet]
    public ActionResult<List<Item>> GetAllItems()
    {
        return Ok(_itemService.GetAll());
    }

    // Obtener un item por ID
    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = _itemService.GetById(id);
        if (item == null)
        {
            return NotFound("Item de Menú no encontrado");
        }
        return Ok(item);
    }

    // Crear un nuevo item
    [HttpPost]
    public ActionResult<Item> NuevoCliente(ItemDTO item)
    {
        // Llamar al método Create del servicio de cliente para dar de alta el nuevo cliente
        Item _item = _itemService.Create(item);
        // Devolver el resultado de llamar al método GetById pasando como parámetro el ID del nuevo cliente
        return CreatedAtAction(nameof(GetById), new { id = _item.Id }, _item);
    }

    // Eliminar un cliente por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var item = _itemService.GetById(id);
        if (item == null)
        {
            return NotFound("Item de Menú no encontrado");
        }

        _itemService.Delete(id);
        return NoContent(); // Retorna un 204 No Content
    }

        // Actualizar un Item en el menú
    [HttpPut("{id}")]
    public ActionResult<Item> UpdateItem(int id, Item updatedItem)
    {
        // Asegurarse de que el ID del Item en la solicitud coincida con el ID del parámetro
        if (id != updatedItem.Id)
        {
            return BadRequest("El ID del Item del Menú en la URL no coincide con el ID del Item en el cuerpo de la solicitud.");
        }

        var item = _itemService.Update(id, updatedItem);

        if (item is null)
        {
            return NotFound(); // Si no se encontró el Item en el Menú, retorna 404 Not Found
        }
        return Ok(item); // Retorna el recurso actualizado
    }
}
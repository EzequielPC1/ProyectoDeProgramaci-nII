// Inicialización de WebApplication
var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios

// Registro de Controladores
builder.Services.AddControllers();

// Registro Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de los servicios
builder.Services.AddSqlServer<DeliveryContext>(builder.Configuration.GetConnectionString("cnDelivery")); // Especificación del nombre de la cadena de conexión
builder.Services.AddScoped<IClienteService, ClienteDbService>(); // Registro del servicio de clientes
builder.Services.AddScoped<IItemService, ItemDbService>(); // servicio de items
builder.Services.AddScoped<IPedidoService, PedidoDbService>(); // y registro del servicio de pedidos

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeo de controladores
app.MapControllers();

app.Run();
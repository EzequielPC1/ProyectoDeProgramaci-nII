// Inicialización de WebApplication
var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios

// Registro de Controladores
builder.Services.AddControllers();

// Registro Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de los servicios
builder.Services.AddSqlServer<DeliveryContext>(builder.Configuration.GetConnectionString("cnDelivery")); // Especifica el nombre de la cadena de conexión
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IClienteService, ClienteDbService>(); // Registro del servicio de clientes
builder.Services.AddScoped<IItemService, ItemDbService>(); // Registro del servicio de items
builder.Services.AddScoped<IPedidoService, PedidoDbService>(); // Registro del servicio de pedidos

var app = builder.Build();

// Configuramos el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeamos los controladores
app.MapControllers();

app.Run();

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


//Se agrega nuestro pequeño script para cargar las envs del archivo .env
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// Inicialización de WebApplication
var builder = WebApplication.CreateBuilder(args);


// Configuración de servicios
// Registro de Controladores
builder.Services.AddControllers();

// Se crea la cadena de conexión a partir de las vairables de sistema.
var connetionString = builder.Configuration.GetConnectionString("cnDelivery");
connetionString = connetionString.Replace("SERVER_NAME", builder.Configuration["SERVER_NAME"]);
connetionString = connetionString.Replace("DB_USER", builder.Configuration["DB_USER"]);
connetionString = connetionString.Replace("DB_PASS", builder.Configuration["DB_PASS"]);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuración básica de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tu API", Version = "v1" });

    // Configuración de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT en este formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Registro Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de los servicios
builder.Services.AddSqlServer<DeliveryContext>(builder.Configuration.GetConnectionString("cnDelivery")); // Especificación del nombre de la cadena de conexión
builder.Services.AddScoped<IClienteService, ClienteDbService>(); // Registro del servicio de clientes
builder.Services.AddScoped<IItemService, ItemDbService>(); // servicio de items
builder.Services.AddScoped<IPedidoService, PedidoDbService>(); // y registro del servicio de pedidos

// Configurar el contexto para Identity (autenticación y autorización)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connetionString));

// Configuración Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar JWT para autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Debes configurar esto en tu appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"],  // Lo mismo aquí
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // La clave secreta
    };
});


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
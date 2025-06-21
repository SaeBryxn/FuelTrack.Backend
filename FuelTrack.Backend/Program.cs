using FuelTrack.Backend.Application.Orders.Interfaces;
using FuelTrack.Backend.Application.Orders.Services;
using FuelTrack.Backend.Domain.Orders.Repositories;
using FuelTrack.Backend.Infrastructure.Persistence;
using FuelTrack.Backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using FuelTrack.Backend.Application.Analytics.Interfaces;
using FuelTrack.Backend.Application.Analytics.Services;
using FuelTrack.Backend.Application.Pricing.Services;
using FuelTrack.Backend.Domain.Pricing.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ Base de datos MySQL (ajusta tu cadena de conexión en appsettings.json)
builder.Services.AddDbContext<FuelTrackDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// ✅ Repositorios y Servicios de Orders
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IClientAnalyticsService, ClientAnalyticsService>();
builder.Services.AddScoped<IFuelPriceRepository, FuelPriceRepository>();
builder.Services.AddScoped<FuelPriceService>();

// ✅ Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ✅ Controllers y Swagger (opcional pero útil)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Pipeline
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FuelTrack API v1");
    c.RoutePrefix = "swagger"; // Esto hace que Swagger esté en /swagger
});


app.UseHttpsRedirection();

// 👇 Agrega esto para habilitar CORS
app.UseCors();

app.MapGet("/", () => "🚀 FuelTrack API está corriendo");

app.MapControllers(); // 👈 Esto activa tus endpoints reales

app.Run();
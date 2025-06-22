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

// ✅ Base de datos PostgreSQL
builder.Services.AddDbContext<FuelTrackDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ✅ Repositorios y Servicios
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IClientAnalyticsService, ClientAnalyticsService>();
builder.Services.AddScoped<IFuelPriceRepository, FuelPriceRepository>();
builder.Services.AddScoped<FuelPriceService>();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Migraciones automáticas al iniciar
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = scope.ServiceProvider.GetRequiredService<FuelTrackDbContext>();
    logger.LogInformation("📦 Ejecutando migraciones en startup...");
    db.Database.Migrate();
    logger.LogInformation("✅ Migraciones completadas.");
}


// ✅ Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FuelTrack API v1");
    c.RoutePrefix = "swagger"; // Visitable en /swagger
});

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/", () => "🚀 FuelTrack API está corriendo");
app.MapControllers();

app.Run();

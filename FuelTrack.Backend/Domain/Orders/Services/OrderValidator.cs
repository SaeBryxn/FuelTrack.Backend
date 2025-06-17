using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Domain.Orders.Services;

/// <summary>
/// Provides domain-level validation logic for orders.
/// </summary>
public static class OrderValidator
{
    public static bool IsValid(Order order, out List<string> errors)
    {
        errors = new();

        if (string.IsNullOrWhiteSpace(order.UserName))
            errors.Add("El nombre del usuario es obligatorio.");

        if (string.IsNullOrWhiteSpace(order.TerminalName))
            errors.Add("Debe seleccionar una terminal.");

        if (!order.Products.Any())
            errors.Add("La orden debe contener al menos un producto.");

        foreach (var p in order.Products)
        {
            if (p.Quantity <= 0)
                errors.Add($"El producto '{p.FuelType}' debe tener una cantidad válida.");
            if (p.UnitPrice <= 0)
                errors.Add($"El producto '{p.FuelType}' debe tener un precio válido.");
        }

        return !errors.Any();
    }
}
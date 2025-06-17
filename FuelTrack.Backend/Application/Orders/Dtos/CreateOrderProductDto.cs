namespace FuelTrack.Backend.Application.Orders.Dtos;

/// <summary>
/// Represents a product in a fuel order (DTO).
/// </summary>
public class CreateOrderProductDto
{
    public string FuelType { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }

    public string? Note { get; set; }

 
    public List<CreatePaymentDto>? Payments { get; set; } // ✅ AGREGAR ESTO
}
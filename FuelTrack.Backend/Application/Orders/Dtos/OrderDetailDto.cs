namespace FuelTrack.Backend.Application.Orders.Dtos;

/// <summary>
/// DTO that returns full details of an order.
/// </summary>
public class OrderDetailsDto
{
    public Guid Id { get; set; }
    public string OrderId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string TerminalName { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderProductDto> Products { get; set; } = new();
    public List<PaymentDto> Payments { get; set; } = new();
}

public class OrderProductDto
{
    public string FuelType { get; set; } = default!;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
    public decimal TotalPrice { get; set; }
    public List<PaymentDto> Payments { get; set; } = new();
}

public class PaymentDto
{
    public string Bank { get; set; } = default!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? OperationCode { get; set; }
}
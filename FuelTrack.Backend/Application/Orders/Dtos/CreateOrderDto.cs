namespace FuelTrack.Backend.Application.Orders.Dtos;

/// <summary>
/// Data Transfer Object for creating a new fuel order.
/// </summary>
public class CreateOrderDto
{
    public string UserName { get; set; } = string.Empty;
    public string TerminalName { get; set; } = string.Empty;
    public List<CreateOrderProductDto> Products { get; set; } = new();
    public List<CreatePaymentDto> Payments { get; set; } = new();
}
namespace FuelTrack.Backend.Application.Orders.Dtos;

public class OrderSummaryDto
{
    public Guid Id { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string TerminalName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
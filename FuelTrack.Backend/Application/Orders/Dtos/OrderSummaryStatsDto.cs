namespace FuelTrack.Backend.Application.Orders.Dtos;

public class OrderSummaryStatsDto
{
    public int TotalOrders { get; set; }
    public Dictionary<string, int> OrdersPerStatus { get; set; } = new();
    public double TotalGallons { get; set; }
    public decimal TotalAmount { get; set; }
}
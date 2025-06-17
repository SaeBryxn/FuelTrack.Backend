namespace FuelTrack.Backend.Application.Analytics.Dtos;

public class OrderSummaryStatsDto
{
    public int TotalOrders { get; set; }
    public Dictionary<string, int> OrdersPerStatus { get; set; } = new();
    public double TotalGallons { get; set; }
    public decimal TotalRevenue { get; set; }
}
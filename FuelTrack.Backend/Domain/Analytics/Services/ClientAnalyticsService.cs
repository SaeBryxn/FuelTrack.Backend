using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Domain.Analytics.Services;

/// <summary>
/// Provides analytics related to client activity across terminals and products.
/// </summary>
public static class ClientAnalyticsService
{
    public static int GetTotalOrders(List<Order> orders)
    {
        return orders.Count;
    }

    public static decimal GetTotalGallons(List<Order> orders)
    {
        return orders.SelectMany(o => o.Products).Sum(p => p.Quantity);
    }

    public static Dictionary<string, int> GetOrdersByTerminal(List<Order> orders)
    {
        return orders
            .GroupBy(o => o.TerminalName)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
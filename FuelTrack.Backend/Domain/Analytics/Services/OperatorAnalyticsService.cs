using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Domain.Analytics.Services;

/// <summary>
/// Provides analytics about the users who created orders (operators).
/// </summary>
public static class OperatorAnalyticsService
{
    public static Dictionary<string, int> GetOrderCountsByOperator(List<Order> orders)
    {
        return orders
            .GroupBy(o => o.UserName)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
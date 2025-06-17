using FuelTrack.Backend.Application.Analytics.Dtos;
using FuelTrack.Backend.Application.Analytics.Interfaces;
using FuelTrack.Backend.Domain.Orders.Repositories;

namespace FuelTrack.Backend.Application.Analytics.Services;

public class ClientAnalyticsService : IClientAnalyticsService
{
    private readonly IOrderRepository _orderRepository;

    public ClientAnalyticsService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderSummaryStatsDto> GetOrderSummaryAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        var summary = new OrderSummaryStatsDto
        {
            TotalOrders = orders.Count,
            OrdersPerStatus = orders
                .GroupBy(o => o.Status.ToString())
                .ToDictionary(g => g.Key, g => g.Count()),
            TotalGallons = orders
                .SelectMany(o => o.Products)
                .Sum(p => (double)p.Quantity),
            TotalRevenue = orders
                .Sum(o => o.GetTotalAmount())
        };

        return summary;
    }
}
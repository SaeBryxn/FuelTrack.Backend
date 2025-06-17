using FuelTrack.Backend.Application.Analytics.Dtos;

namespace FuelTrack.Backend.Application.Analytics.Interfaces;

public interface IClientAnalyticsService
{
    Task<OrderSummaryStatsDto> GetOrderSummaryAsync();
}
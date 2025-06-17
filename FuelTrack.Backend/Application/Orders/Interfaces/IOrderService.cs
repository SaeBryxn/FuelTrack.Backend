using FuelTrack.Backend.Application.Orders.Dtos;
using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Application.Orders.Interfaces;

public interface IOrderService
{
    Task<(bool Success, string? ErrorMessage, Guid? OrderId)> CreateOrderAsync(CreateOrderDto dto);
    Task<List<OrderSummaryDto>> GetAllAsync(string? status = null);
    Task<OrderDetailsDto?> GetOrderDetailsByIdAsync(Guid id);
    Task<bool> ApproveAsync(Guid id);
    Task<bool> ReleaseAsync(Guid id);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task<bool> AssignTransportAsync(Guid orderId, AssignTransportDto dto);
    
    Task<List<OrderDetailsDto>> GetAllRawAsync();

}
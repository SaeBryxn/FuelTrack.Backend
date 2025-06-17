using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Domain.Orders.Repositories;

/// <summary>
/// Contract for order persistence operations.
/// </summary>
public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
    Task DeleteAsync(Order order);

}
using FuelTrack.Backend.Domain.Orders.Entities;
using FuelTrack.Backend.Domain.Orders.Repositories;
using FuelTrack.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FuelTrack.Backend.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements persistence operations for fuel orders.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly FuelTrackDbContext _context;

    public OrderRepository(FuelTrackDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Products)
            .Include(o => o.Payments)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Products)
            .Include(o => o.Payments)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

}
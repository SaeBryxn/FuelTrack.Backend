using FuelTrack.Backend.Domain.Orders.Entities;

namespace FuelTrack.Backend.Domain.Orders.Events;

/// <summary>
/// Domain event triggered when an order is approved.
/// </summary>
public class OrderApprovedEvent
{
    public Order Order { get; }

    public DateTime ApprovedAt { get; }

    public OrderApprovedEvent(Order order)
    {
        Order = order;
        ApprovedAt = DateTime.UtcNow;
    }
}
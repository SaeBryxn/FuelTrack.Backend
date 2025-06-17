namespace FuelTrack.Backend.Domain.Orders.ValueObjects;

/// <summary>
/// Represents the current status of an order.
/// </summary>
public enum OrderStatus
{
    Requested = 0,
    Approved = 1,
    Released = 2
}
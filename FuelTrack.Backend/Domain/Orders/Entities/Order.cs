using FuelTrack.Backend.Domain.Orders.ValueObjects;
using FuelTrack.Backend.Domain.Shared;

namespace FuelTrack.Backend.Domain.Orders.Entities;

/// <summary>
/// Represents a fuel order created by a client, including ordered products and associated payments.
/// </summary>
public class Order : BaseEntity
{
    // 🔒 Public ID for external reference (not the DB primary key)
    public string OrderId { get; private set; } = Guid.NewGuid().ToString("N");

    // 👤 Client details
    public string UserName { get; private set; } = string.Empty;
    public string TerminalName { get; private set; } = string.Empty;

    // 🔁 Order status (Requested → Approved → Released)
    public OrderStatus Status { get; private set; } = OrderStatus.Requested;

    // 📦 Ordered products and associated payments
    public List<OrderProduct> Products { get; private set; } = new();
    public List<Payment> Payments { get; private set; } = new();

    // ⚙️ Required by EF Core (DO NOT REMOVE)
    private Order() { }

    /// <summary>
    /// Domain constructor used for creating new orders.
    /// </summary>
    public Order(string userName, string terminalName, List<OrderProduct> products)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username is required.", nameof(userName));

        if (string.IsNullOrWhiteSpace(terminalName))
            throw new ArgumentException("Terminal name is required.", nameof(terminalName));

        if (products == null || !products.Any())
            throw new ArgumentException("At least one product is required.", nameof(products));

        UserName = userName;
        TerminalName = terminalName;
        Products = products;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    /// <summary>
    /// Calculates the total amount for all products in the order.
    /// </summary>
    public decimal GetTotalAmount() => Products.Sum(p => p.TotalPrice);

    /// <summary>
    /// Registers a payment for the order.
    /// </summary>
    public void AddPayment(Payment payment)
    {
        if (payment == null) throw new ArgumentNullException(nameof(payment));

        Payments.Add(payment);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the order as approved.
    /// </summary>
    public void Approve()
    {
        if (Status != OrderStatus.Requested)
            throw new InvalidOperationException("Only requested orders can be approved.");

        Status = OrderStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Releases the order for dispatch.
    /// </summary>
    public void Release()
    {
        if (Status != OrderStatus.Approved)
            throw new InvalidOperationException("Only approved orders can be released.");

        Status = OrderStatus.Released;
        UpdatedAt = DateTime.UtcNow;
    }
    
    
    public Guid? TruckId { get; private set; }
    public Guid? DriverId { get; private set; }
    public Guid? TankId { get; private set; }

    public void AssignTransport(Guid truckId, Guid driverId, Guid tankId)
    {
        TruckId = truckId;
        DriverId = driverId;
        TankId = tankId;
        UpdatedAt = DateTime.UtcNow;
    }

}

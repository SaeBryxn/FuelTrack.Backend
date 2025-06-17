using FuelTrack.Backend.Domain.Orders.ValueObjects;

namespace FuelTrack.Backend.Domain.Orders.Entities;

/// <summary>
/// Represents a product included in a fuel order.
/// </summary>
public class OrderProduct
{
    public Guid Id { get; private set; } = Guid.NewGuid(); // ✅ EF Core requiere Id

    public string FuelType { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string? Note { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public List<Payment> Payments { get; private set; } = new();

    // Constructor para EF
    private OrderProduct() { }

    public OrderProduct(string fuelType, decimal quantity, string unit, decimal unitPrice, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(fuelType))
            throw new ArgumentException("Fuel type cannot be empty.");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero.");

        FuelType = fuelType;
        Quantity = quantity;
        Unit = unit;
        UnitPrice = unitPrice;
        Note = note;
    }

    public void AddPayment(Payment payment)
    {
        Payments.Add(payment);
    }

    public decimal GetPaidAmount()
    {
        return Payments.Sum(p => p.Amount);
    }

    public bool IsFullyPaid()
    {
        return GetPaidAmount() >= TotalPrice;
    }
}
using FuelTrack.Backend.Domain.Shared;

namespace FuelTrack.Backend.Domain.Pricing.Entities;

/// <summary>
/// Represents the unit price of a fuel type at a specific terminal.
/// </summary>
public class FuelPrice : BaseEntity
{
    public string FuelType { get; private set; }
    public string TerminalName { get; private set; }
    public decimal UnitPrice { get; private set; }

    public FuelPrice(string fuelType, string terminalName, decimal unitPrice)
    {
        FuelType = fuelType;
        TerminalName = terminalName;
        UnitPrice = unitPrice;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void UpdatePrice(decimal newPrice)
    {
        UnitPrice = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }
    
}
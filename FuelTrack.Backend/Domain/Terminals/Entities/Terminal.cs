using FuelTrack.Backend.Domain.Shared;
using FuelTrack.Backend.Domain.Terminals.ValueObjects;

namespace FuelTrack.Backend.Domain.Terminals.Entities;

/// <summary>
/// Represents a fuel terminal where orders are dispatched from.
/// </summary>
public class Terminal : BaseEntity
{
    public string Name { get; private set; }
    public int TotalOrders { get; private set; }
    public decimal TotalGallons { get; private set; }
    public TerminalStatus Status { get; private set; }

    public Terminal(string name)
    {
        Name = name;
        TotalOrders = 0;
        TotalGallons = 0;
        Status = TerminalStatus.Idle;
    }

    public void RegisterOrder(decimal gallons)
    {
        TotalOrders++;
        TotalGallons += gallons;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TerminalStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}
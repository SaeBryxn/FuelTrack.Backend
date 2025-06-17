using FuelTrack.Backend.Domain.Shared;

namespace FuelTrack.Backend.Domain.Dispatch.Entities;

/// <summary>
/// Represents a fuel transport truck.
/// </summary>
public class Truck : BaseEntity
{
    public string Plate { get; private set; }
    public string FuelType { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public Truck(string plate, string fuelType)
    {
        Plate = plate;
        FuelType = fuelType;
    }

    public void MarkUnavailable()
    {
        IsAvailable = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAvailable()
    {
        IsAvailable = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
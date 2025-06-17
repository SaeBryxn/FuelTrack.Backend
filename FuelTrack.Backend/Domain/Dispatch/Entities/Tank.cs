using FuelTrack.Backend.Domain.Shared;
using FuelTrack.Backend.Domain.Dispatch.ValueObjects;

namespace FuelTrack.Backend.Domain.Dispatch.Entities;

/// <summary>
/// Represents a fuel tank trailer with multiple compartments.
/// </summary>
public class Tank : BaseEntity
{
    public string SerialNumber { get; private set; }
    public int TotalCompartments { get; private set; }

    public List<CompartmentAssignment> CompartmentAssignments { get; private set; } = new();

    public Tank(string serialNumber, int totalCompartments)
    {
        SerialNumber = serialNumber;
        TotalCompartments = totalCompartments;
    }

    public void AssignCompartment(int number, string fuelType, decimal gallons)
    {
        if (number < 1 || number > TotalCompartments)
            throw new ArgumentException("Invalid compartment number.");

        CompartmentAssignments.Add(new CompartmentAssignment(number, fuelType, gallons));
        UpdatedAt = DateTime.UtcNow;
    }
}
namespace FuelTrack.Backend.Domain.Dispatch.ValueObjects;

/// <summary>
/// Represents a product assignment to a specific compartment in the tank.
/// </summary>
public class CompartmentAssignment
{
    public int CompartmentNumber { get; private set; }
    public string FuelType { get; private set; }
    public decimal Gallons { get; private set; }

    public CompartmentAssignment(int compartmentNumber, string fuelType, decimal gallons)
    {
        CompartmentNumber = compartmentNumber;
        FuelType = fuelType;
        Gallons = gallons;
    }
}
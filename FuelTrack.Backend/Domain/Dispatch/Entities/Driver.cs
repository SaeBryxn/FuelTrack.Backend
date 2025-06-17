using FuelTrack.Backend.Domain.Shared;

namespace FuelTrack.Backend.Domain.Dispatch.Entities;

/// <summary>
/// Represents a truck driver available for fuel dispatch operations.
/// </summary>
public class Driver : BaseEntity
{
    public string FullName { get; private set; }
    public string LicenseNumber { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public Driver(string fullName, string licenseNumber)
    {
        FullName = fullName;
        LicenseNumber = licenseNumber;
    }

    public void Assign()
    {
        IsAvailable = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Release()
    {
        IsAvailable = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
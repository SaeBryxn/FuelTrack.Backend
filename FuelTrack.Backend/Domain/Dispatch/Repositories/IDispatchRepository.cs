using FuelTrack.Backend.Domain.Dispatch.Entities;

namespace FuelTrack.Backend.Domain.Dispatch.Repositories;

/// <summary>
/// Contract for managing dispatch entities: trucks, drivers, tanks.
/// </summary>
public interface IDispatchRepository
{
    Task<List<Truck>> GetAvailableTrucksAsync(string fuelType);
    Task<List<Driver>> GetAvailableDriversAsync();
    Task<List<Tank>> GetAvailableTanksAsync();

    Task AssignTruckAsync(Truck truck);
    Task AssignDriverAsync(Driver driver);
    Task AssignTankAsync(Tank tank);

    Task SaveChangesAsync();
}
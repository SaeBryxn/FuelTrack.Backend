using FuelTrack.Backend.Domain.Pricing.Entities;

namespace FuelTrack.Backend.Domain.Pricing.Repositories;

public interface IFuelPriceRepository
{
    Task<FuelPrice?> GetByFuelTypeAndTerminalAsync(string fuelType, string terminalName);
    Task AddAsync(FuelPrice fuelPrice);
    Task UpdateAsync(FuelPrice fuelPrice);
}
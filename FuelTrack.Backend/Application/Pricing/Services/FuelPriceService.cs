using FuelTrack.Backend.Domain.Pricing.Entities;
using FuelTrack.Backend.Domain.Pricing.Repositories;

namespace FuelTrack.Backend.Application.Pricing.Services;

public class FuelPriceService
{
    private readonly IFuelPriceRepository _repository;

    public FuelPriceService(IFuelPriceRepository repository)
    {
        _repository = repository;
    }

    public async Task<FuelPrice?> GetAsync(string fuelType, string terminalName)
    {
        return await _repository.GetByFuelTypeAndTerminalAsync(fuelType, terminalName);
    }

    public async Task<bool> UpdatePriceAsync(string fuelType, string terminalName, decimal newPrice)
    {
        var price = await _repository.GetByFuelTypeAndTerminalAsync(fuelType, terminalName);
        if (price == null) return false;

        price.UpdatePrice(newPrice);
        await _repository.UpdateAsync(price);
        return true;
    }

    public async Task CreateAsync(string fuelType, string terminalName, decimal price)
    {
        var newPrice = new FuelPrice(fuelType, terminalName, price);
        await _repository.AddAsync(newPrice);
    }
}
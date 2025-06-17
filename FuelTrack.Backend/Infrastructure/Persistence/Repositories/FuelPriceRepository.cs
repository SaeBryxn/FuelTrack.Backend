using FuelTrack.Backend.Domain.Pricing.Entities;
using FuelTrack.Backend.Domain.Pricing.Repositories;
using Microsoft.EntityFrameworkCore;
using FuelTrack.Backend.Infrastructure.Persistence;

namespace FuelTrack.Backend.Infrastructure.Persistence.Repositories;

public class FuelPriceRepository : IFuelPriceRepository
{
    private readonly FuelTrackDbContext _context;

    public FuelPriceRepository(FuelTrackDbContext context)
    {
        _context = context;
    }

    public async Task<FuelPrice?> GetByFuelTypeAndTerminalAsync(string fuelType, string terminalName)
    {
        return await _context.FuelPrices
            .FirstOrDefaultAsync(p => p.FuelType == fuelType && p.TerminalName == terminalName);
    }

    public async Task AddAsync(FuelPrice fuelPrice)
    {
        await _context.FuelPrices.AddAsync(fuelPrice);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FuelPrice fuelPrice)
    {
        _context.FuelPrices.Update(fuelPrice);
        await _context.SaveChangesAsync();
    }
}
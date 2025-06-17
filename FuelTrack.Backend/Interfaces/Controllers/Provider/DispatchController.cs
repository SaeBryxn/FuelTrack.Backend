using FuelTrack.Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuelTrack.Backend.Interfaces.Rest.Dispatch;

[ApiController]
[Route("api/dispatch/lookup")]
public class DispatchLookupController : ControllerBase
{
    private readonly FuelTrackDbContext _context;

    public DispatchLookupController(FuelTrackDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableResources()
    {
        var trucks = await _context.Trucks
            .Where(t => t.IsAvailable)
            .Select(t => new { t.Id, t.Plate, t.FuelType })
            .ToListAsync();

        var drivers = await _context.Drivers
            .Where(d => d.IsAvailable)
            .Select(d => new { d.Id, d.FullName, d.LicenseNumber })
            .ToListAsync();

        var tanks = await _context.Tanks
            .Select(t => new { t.Id, t.SerialNumber, t.TotalCompartments })
            .ToListAsync();

        return Ok(new
        {
            trucks,
            drivers,
            tanks
        });
    }
}
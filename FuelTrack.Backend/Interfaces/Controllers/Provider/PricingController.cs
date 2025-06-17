using FuelTrack.Backend.Application.Pricing.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Backend.Interfaces.Controllers.Provider;

[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly FuelPriceService _fuelPriceService;

    public PricingController(FuelPriceService fuelPriceService)
    {
        _fuelPriceService = fuelPriceService;
    }

    /// <summary> Updates the unit price of a fuel type at a terminal. </summary>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateFuelPrice([FromQuery] string fuelType, [FromQuery] string terminal, [FromQuery] decimal newPrice)
    {
        var success = await _fuelPriceService.UpdatePriceAsync(fuelType, terminal, newPrice);
        if (!success) return NotFound(new { message = "Fuel price not found." });

        return NoContent();
    }

    /// <summary> Creates a new fuel price entry. </summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreateFuelPrice([FromQuery] string fuelType, [FromQuery] string terminal, [FromQuery] decimal price)
    {
        await _fuelPriceService.CreateAsync(fuelType, terminal, price);
        return Ok(new { message = "Created successfully." });
    }

    /// <summary> Gets the current price for a fuel type at a terminal. </summary>
    [HttpGet]
    public async Task<IActionResult> GetFuelPrice([FromQuery] string fuelType, [FromQuery] string terminal)
    {
        var price = await _fuelPriceService.GetAsync(fuelType, terminal);
        return price == null ? NotFound() : Ok(price);
    }
}
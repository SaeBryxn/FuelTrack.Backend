using FuelTrack.Backend.Application.Analytics.Interfaces;
using FuelTrack.Backend.Application.Analytics.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Backend.Interfaces.Controllers.Client;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IClientAnalyticsService _analyticsService;

    public AnalyticsController(IClientAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Returns statistical summary of all orders.
    /// </summary>
    [HttpGet("summary")]
    [ProducesResponseType(typeof(OrderSummaryStatsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderSummary()
    {
        var summary = await _analyticsService.GetOrderSummaryAsync();
        return Ok(summary);
    }
}
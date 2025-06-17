namespace FuelTrack.Backend.Application.Pricing.Dtos;

public class UpdateFuelPriceDto
{
    public string FuelType { get; set; } = default!;
    public string TerminalName { get; set; } = default!;
    public decimal NewPrice { get; set; }
}

namespace FuelTrack.Backend.Application.Orders.Dtos;

public class CreatePaymentDto
{
    public string Bank { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
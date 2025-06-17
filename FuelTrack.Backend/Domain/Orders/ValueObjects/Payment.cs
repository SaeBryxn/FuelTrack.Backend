namespace FuelTrack.Backend.Domain.Orders.ValueObjects;

/// <summary>
/// Represents a payment made by the client for a specific order.
/// </summary>
public class Payment
{
    public Guid Id { get; private set; } = Guid.NewGuid(); // Necesario para EF
    public string Bank { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string OperationCode { get; private set; } = string.Empty;

    public Payment(string bank, decimal amount, DateTime date)
    {
        Bank = bank;
        Amount = amount;
        Date = date;
        OperationCode = GenerateOperationCode();
    }

    // Constructor sin parámetros requerido por EF Core
    private Payment() { }

    private string GenerateOperationCode()
    {
        return $"OP-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
    }
}
namespace FuelTrack.Backend.Application.Orders.Dtos;

public class AssignTransportDto
{
    public Guid TruckId { get; set; }
    public Guid DriverId { get; set; }
    public Guid TankId { get; set; }
}
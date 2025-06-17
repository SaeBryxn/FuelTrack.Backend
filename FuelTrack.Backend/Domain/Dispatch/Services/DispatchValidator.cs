using FuelTrack.Backend.Domain.Dispatch.Entities;

namespace FuelTrack.Backend.Domain.Dispatch.Services;

/// <summary>
/// Validates dispatch assignments before releasing an order.
/// </summary>
public static class DispatchValidator
{
    public static bool IsValid(Truck truck, Driver driver, Tank tank, out List<string> errors)
    {
        errors = new();

        if (truck == null || !truck.IsAvailable)
            errors.Add("El camión no está disponible o no ha sido asignado.");

        if (driver == null || !driver.IsAvailable)
            errors.Add("El conductor no está disponible o no ha sido asignado.");

        if (tank == null || tank.CompartmentAssignments.Count == 0)
            errors.Add("Debe asignar al menos un compartimiento al tanque.");

        var duplicatedCompartments = tank.CompartmentAssignments
            .GroupBy(c => c.CompartmentNumber)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicatedCompartments.Any())
            errors.Add("Hay compartimientos duplicados en el tanque.");

        return !errors.Any();
    }
}
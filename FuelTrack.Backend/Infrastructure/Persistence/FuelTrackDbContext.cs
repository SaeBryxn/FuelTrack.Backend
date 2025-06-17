using FuelTrack.Backend.Domain.Orders.Entities;
using FuelTrack.Backend.Domain.Orders.ValueObjects;
using FuelTrack.Backend.Domain.Dispatch.Entities;
using FuelTrack.Backend.Domain.Dispatch.ValueObjects;
using FuelTrack.Backend.Domain.Pricing.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuelTrack.Backend.Infrastructure.Persistence;

public class FuelTrackDbContext : DbContext
{
    public FuelTrackDbContext(DbContextOptions<FuelTrackDbContext> options)
        : base(options) { }

    // 🟢 DbSets
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Truck> Trucks => Set<Truck>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Tank> Tanks => Set<Tank>();
    public DbSet<FuelPrice> FuelPrices => Set<FuelPrice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🟢 Order
        modelBuilder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);

            order.Property(o => o.OrderId).IsRequired();
            order.Property(o => o.UserName).IsRequired();
            order.Property(o => o.TerminalName).IsRequired();

            order.OwnsMany(o => o.Products, product =>
            {
                product.WithOwner();

                product.Property(p => p.FuelType).IsRequired();
                product.Property(p => p.Quantity).IsRequired();
                product.Property(p => p.Unit).IsRequired();
                product.Property(p => p.UnitPrice).IsRequired();
                product.Property(p => p.Note);

                product.OwnsMany(p => p.Payments, payment =>
                {
                    payment.WithOwner();
                    payment.Property(p => p.Bank).IsRequired();
                    payment.Property(p => p.Amount).IsRequired();
                    payment.Property(p => p.Date).IsRequired();
                    payment.Property(p => p.OperationCode).IsRequired();
                });
            });

            order.OwnsMany(o => o.Payments, payment =>
            {
                payment.WithOwner();
                payment.Property(p => p.Bank).IsRequired();
                payment.Property(p => p.Amount).IsRequired();
                payment.Property(p => p.Date).IsRequired();
                payment.Property(p => p.OperationCode).IsRequired();
            });
        });

        // 🟢 Truck
        modelBuilder.Entity<Truck>(truck =>
        {
            truck.HasKey(t => t.Id);
            truck.Property(t => t.Plate).IsRequired();
            truck.Property(t => t.FuelType).IsRequired();
            truck.Property(t => t.IsAvailable).IsRequired();
        });

        // 🟢 Driver
        modelBuilder.Entity<Driver>(driver =>
        {
            driver.HasKey(d => d.Id);
            driver.Property(d => d.FullName).IsRequired();
            driver.Property(d => d.LicenseNumber).IsRequired();
            driver.Property(d => d.IsAvailable).IsRequired();
        });

        // 🟢 Tank
        modelBuilder.Entity<Tank>(tank =>
        {
            tank.HasKey(t => t.Id);
            tank.Property(t => t.SerialNumber).IsRequired();
            tank.Property(t => t.TotalCompartments).IsRequired();

            tank.OwnsMany(t => t.CompartmentAssignments, assignment =>
            {
                assignment.WithOwner();
                assignment.Property(a => a.CompartmentNumber).IsRequired();
                assignment.Property(a => a.FuelType).IsRequired();
                assignment.Property(a => a.Gallons).IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}

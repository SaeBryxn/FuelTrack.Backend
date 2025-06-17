using FuelTrack.Backend.Application.Orders.Dtos;
using FuelTrack.Backend.Application.Orders.Interfaces;
using FuelTrack.Backend.Domain.Orders.Entities;
using FuelTrack.Backend.Domain.Orders.Repositories;
using FuelTrack.Backend.Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FuelTrack.Backend.Application.Orders.Services;

/// <summary>
/// Handles the logic for creating and managing fuel orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Success, string? ErrorMessage, Guid? OrderId)> CreateOrderAsync(CreateOrderDto dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.TerminalName))
                return (false, "Invalid order data. Username and terminal are required.", null);

            if (dto.Products == null || !dto.Products.Any())
                return (false, "At least one product must be provided.", null);

            // Map products
            var products = dto.Products.Select(p =>
            {
                var product = new OrderProduct(
                    fuelType: p.FuelType,
                    quantity: p.Quantity,
                    unit: p.Unit,
                    unitPrice: p.UnitPrice,
                    note: p.Note
                );

                if (p.Payments != null)
                {
                    foreach (var paymentDto in p.Payments)
                    {
                        var payment = new Payment(
                            bank: paymentDto.Bank,
                            amount: paymentDto.Amount,
                            date: paymentDto.Date
                        );
                        product.AddPayment(payment);
                    }
                }

                return product;
            }).ToList();

            // Crear orden
            var order = new Order(dto.UserName, dto.TerminalName, products);

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            return (true, null, order.Id);
        }
        catch (DbUpdateException dbEx)
        {
            return (false, $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}", null);
        }
        catch (Exception ex)
        {
            return (false, $"Unexpected error: {ex.Message}", null);
        }
    }

    public async Task<List<OrderSummaryDto>> GetAllAsync(string? status = null)
    {
        var orders = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
        {
            orders = orders.Where(o => o.Status == parsedStatus).ToList();
        }

        return orders.Select(order => new OrderSummaryDto
        {
            Id = order.Id,
            OrderId = order.OrderId,
            UserName = order.UserName,
            TerminalName = order.TerminalName,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalAmount = order.GetTotalAmount()
        }).ToList();
    }



    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> ApproveAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;

        order.Approve();
        await _repository.SaveChangesAsync();
        return true;
    }


    public async Task<bool> ReleaseAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;

        order.Release();
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<OrderDetailsDto?> GetOrderDetailsByIdAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return null;

        return new OrderDetailsDto
        {
            Id = order.Id,
            OrderId = order.OrderId,
            UserName = order.UserName,
            TerminalName = order.TerminalName,
            Status = order.Status.ToString(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Payments = order.Payments?.Select(p => new PaymentDto
            {
                Bank = p.Bank,
                Amount = p.Amount,
                Date = p.Date,
                OperationCode = p.OperationCode
            }).ToList() ?? new List<PaymentDto>(),

            Products = order.Products?.Select(prod => new OrderProductDto
            {
                FuelType = prod.FuelType,
                Quantity = prod.Quantity,
                Unit = prod.Unit,
                UnitPrice = prod.UnitPrice,
                Note = prod.Note,
                TotalPrice = prod.TotalPrice,
                Payments = prod.Payments?.Select(pp => new PaymentDto
                {
                    Bank = pp.Bank,
                    Amount = pp.Amount,
                    Date = pp.Date,
                    OperationCode = pp.OperationCode
                }).ToList() ?? new List<PaymentDto>()
            }).ToList() ?? new List<OrderProductDto>()
        };
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;

        // Solo se permite eliminar si está en estado Requested
        if (order.Status != OrderStatus.Requested)
            return false;

        await _repository.DeleteAsync(order);
        return true;
    }

    public async Task<bool> AssignTransportAsync(Guid orderId, AssignTransportDto dto)
    {
        var order = await _repository.GetByIdAsync(orderId);
        if (order == null || order.Status != OrderStatus.Approved) return false;

        order.AssignTransport(dto.TruckId, dto.DriverId, dto.TankId);
        await _repository.SaveChangesAsync();
        return true;
    }
    public async Task<List<OrderDetailsDto>> GetAllRawAsync()
    {
        var orders = await _repository.GetAllAsync();

        return orders.Select(order => new OrderDetailsDto
        {
            Id = order.Id,
            OrderId = order.OrderId,
            UserName = order.UserName,
            TerminalName = order.TerminalName,
            Status = order.Status.ToString(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Payments = order.Payments?.Select(p => new PaymentDto
            {
                Bank = p.Bank,
                Amount = p.Amount,
                Date = p.Date,
                OperationCode = p.OperationCode
            }).ToList() ?? new List<PaymentDto>(),

            Products = order.Products?.Select(prod => new OrderProductDto
            {
                FuelType = prod.FuelType,
                Quantity = prod.Quantity,
                Unit = prod.Unit,
                UnitPrice = prod.UnitPrice,
                Note = prod.Note,
                TotalPrice = prod.TotalPrice,
                Payments = prod.Payments?.Select(pp => new PaymentDto
                {
                    Bank = pp.Bank,
                    Amount = pp.Amount,
                    Date = pp.Date,
                    OperationCode = pp.OperationCode
                }).ToList() ?? new List<PaymentDto>()
            }).ToList() ?? new List<OrderProductDto>()
        }).ToList();
    }

}

using FuelTrack.Backend.Application.Orders.Dtos;
using FuelTrack.Backend.Application.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Backend.Interfaces.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // ============================
    // 📌 CREACIÓN
    // ============================

    /// <summary>
    /// Crea una nueva orden de combustible.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.CreateOrderAsync(dto);

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage });

        return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderId }, new { orderId = result.OrderId });
    }

    // ============================
    // 📌 LECTURA (GET)
    // ============================

    /// <summary>
    /// Obtiene los detalles completos de una orden por ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var result = await _orderService.GetOrderDetailsByIdAsync(id);
        if (result == null)
            return NotFound(new { error = "Order not found." });

        return Ok(result);
    }

    /// <summary>
    /// Lista de órdenes con opción de filtrar por estado (Requested, Approved, etc).
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<OrderSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders([FromQuery] string? status)
    {
        var orders = await _orderService.GetAllAsync(status);
        return Ok(orders);
    }

    /// <summary>
    /// Lista completa de órdenes sin filtro de estado (modo raw).
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<OrderDetailsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRawOrders()
    {
        var orders = await _orderService.GetAllRawAsync();
        return Ok(orders);
    }

    // ============================
    // 📌 ACCIONES SOBRE ORDEN
    // ============================

    /// <summary>
    /// Aprueba una orden existente (estado pasa a Approved).
    /// </summary>
    [HttpPut("{id}/approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveOrder(Guid id)
    {
        var success = await _orderService.ApproveAsync(id);
        if (!success)
            return NotFound(new { error = "Order not found." });

        return NoContent();
    }

    /// <summary>
    /// Libera una orden previamente aprobada (estado pasa a Released).
    /// </summary>
    [HttpPut("{id}/release")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReleaseOrder(Guid id)
    {
        var success = await _orderService.ReleaseAsync(id);
        if (!success)
            return NotFound(new { error = "Order not found or not approved." });

        return NoContent();
    }

    /// <summary>
    /// Asigna transporte a una orden ya aprobada.
    /// </summary>
    [HttpPut("{id}/assign")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignTransport(Guid id, [FromBody] AssignTransportDto dto)
    {
        var success = await _orderService.AssignTransportAsync(id, dto);
        if (!success)
            return NotFound(new { error = "Order not found or not approved." });

        return NoContent();
    }

    // ============================
    // 📌 ELIMINACIÓN
    // ============================

    /// <summary>
    /// Elimina una orden si aún no ha sido aprobada.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var success = await _orderService.DeleteAsync(id);
        if (!success)
            return NotFound(new { error = "Order not found or cannot be deleted." });

        return NoContent();
    }
}

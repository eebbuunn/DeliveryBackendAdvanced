﻿using delivery_backend_advanced.Models.Dtos;
using delivery_backend_advanced.Models.Enums;
using delivery_backend_advanced.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace delivery_backend_advanced.Controllers;

[Route("api/courier")]
public class CourierController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICourierService _courierService;

    public CourierController(IOrderService orderService, ICourierService courierService)
    {
        _orderService = orderService;
        _courierService = courierService;
    }

    /// <summary>
    /// Get list of courier's orders (current == true => taken, else => available)
    /// </summary>
    [HttpGet]
    [Route("orders")]
    public async Task<OrdersPageDto> GetCourierOrders([FromQuery] OrderQueryModel query)
    {
        return await _orderService.GetOrders(query, UserRole.Courier);
    }

    /// <summary>
    /// Change order status when delivered it
    /// </summary>
    [HttpPut]
    [Route("{orderId}")]
    public async Task SetOrderStatusDelivered(Guid orderId)
    {
        await _courierService.SetOrderDelivered(orderId);
    }

    /// <summary>
    /// Courier takes order
    /// </summary>
    [HttpPost]
    [Route("{orderId}")]
    public async Task TakeOrder(Guid orderId)
    {
        await _courierService.TakeOrder(orderId);
    }
    
    /// <summary>
    /// Courier takes order
    /// </summary>
    [HttpPost]
    [Route("cancel/{orderId}")]
    public async Task CancelOrder(Guid orderId)
    {
        await _courierService.CancelOrder(orderId);
    }
}
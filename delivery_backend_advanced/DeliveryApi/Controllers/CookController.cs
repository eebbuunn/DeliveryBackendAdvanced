﻿using delivery_backend_advanced.Models.Dtos;
using delivery_backend_advanced.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace delivery_backend_advanced.Controllers;

[Route("api/cook")]
public class CookController : ControllerBase
{
    private readonly ICookService _cookService;
    private readonly IOrderService _orderService;

    public CookController(ICookService cookService, IOrderService orderService)
    {
        _cookService = cookService;
        _orderService = orderService;
    }

    /// <summary>
    /// Get list of cook's orders
    /// </summary>
    [HttpGet]
    [Route("orders")]
    public async Task<OrdersPageDto> GetCookOrders([FromQuery] OrderQueryModel query)
    {
        query.role = "cook";
        query.current = true;

        return await _orderService.GetOrders(query);
    }
    
    /// <summary>
    /// Get list of available orders
    /// </summary>
    [HttpGet]
    [Route("orders/available")]
    public async Task<OrdersPageDto> GetAvailableOrders([FromQuery] OrderQueryModel query)
    {
        query.role = "cook";
        query.current = false;

        return await _orderService.GetOrders(query);
    }

    /// <summary>
    /// Change status of order when cooked or packaged
    /// </summary>
    [HttpPatch]
    [Route("{orderId}")]
    public async Task ChangeOrderStatus(Guid orderId)
    {
        await _cookService.ChangeOrderStatus(orderId);
    }

    /// <summary>
    /// Cook takes order
    /// </summary>
    [HttpPost]
    [Route("{orderId}")]
    public async Task TakeOrder(Guid orderId)
    {
        await _cookService.TakeOrder(orderId);
    }
}
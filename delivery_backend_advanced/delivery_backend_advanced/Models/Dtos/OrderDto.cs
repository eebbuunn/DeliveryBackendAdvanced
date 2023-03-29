﻿using System.ComponentModel.DataAnnotations;
using delivery_backend_advanced.Models.Entities;
using delivery_backend_advanced.Models.Enums;

namespace delivery_backend_advanced.Models.Dtos;

public class OrderDto
{
    public Guid id { get; set; }
    
    [Required]
    public DateTime deliveryTime { get; set; }
    
    [Required]
    public DateTime orderTime { get; set; }
    
    [Required]
    public double price { get; set; }
    
    [Required]
    public string address { get; set; }
    
    [Required]
    public OrderStatus status { get; set; }

    public List<DishInOrderDto> dishes { get; set; } = new();
    
    // [Required]
    // public CustomerEntity customer { get; set; }
    
    //[Required]
    // public CookEntity cook { get; set; }
    
    // [Required]
    // public CourierEntity courier { get; set; }
}
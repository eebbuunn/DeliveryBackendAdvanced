﻿using System.ComponentModel.DataAnnotations;

namespace delivery_backend_advanced.Models.Entities;

public class Cook
{
    public Guid Id { get; set; }
    
    public List<OrderEntity> Orders { get; set; }
    
    public RestaurantEntity? Restaurant { get; set; }
}
﻿using delivery_backend_advanced.Models.Enums;

namespace delivery_backend_advanced.Models.Dtos;

public class OrderStatusMessage
{
    public Guid UserId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public string Text { get; set; }
    
    public NotificationStatus Status { get; set; }
}
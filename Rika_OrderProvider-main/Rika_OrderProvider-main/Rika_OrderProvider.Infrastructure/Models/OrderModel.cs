using Rika_OrderProvider.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rika_OrderProvider.Infrastructure.Models;

public class OrderModel
{
    [Required]
    public string TotalAmount { get; set; } = null!;

    [Required]
    public string PaymentMethod { get; set; } = null!;

    [Required]
    public string ShipmentMethod { get; set; } = null!;

    public OrderCustomerModel OrderCustomer { get; set; } = new OrderCustomerModel();
    public OrderAddressModel OrderAddress { get; set; } = new OrderAddressModel();

    public ICollection<OrderProductModel> OrderProducts { get; set; } = [];
}
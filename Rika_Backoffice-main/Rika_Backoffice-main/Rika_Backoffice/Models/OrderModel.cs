using System.Text.Json.Serialization;

namespace Rika_Backoffice.Models;

public class OrderModel
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public string? TotalAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ShipmentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public OrderCustomer OrderCustomer { get; set; } = null!;
    public OrderAddress? OrderAddress { get; set; }
    public List<OrderProduct>? OrderProducts { get; set; }
    public int ProductCount => OrderProducts?.Count ?? 0;
}

public class OrderCustomer
{
    public string CustomerName { get; set; } = null!;
    public string? CustomerEmail { get; set; }
    public string? CustomerPhone { get; set; }
}

public class OrderAddress
{
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}

public class OrderProduct
{
    public string? ArticleNumber { get; set; }
    public string? ProductName { get; set; }
    public string? Quantity { get; set; }
    public string? UnitPrice { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
}
public class GetOrderModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string TotalAmount { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string ShipmentMethod { get; set; } = null!;
    public string OrderStatus { get; set; } = null!;
    public OrderCustomerModel OrderCustomer { get; set; } = null!;
    public OrderAddressModel OrderAddress { get; set; } = null!;
    public ICollection<OrderProductModel> OrderProducts { get; set; } = null!;
}

public class OrderCustomerModel
{
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public string CustomerPhone { get; set; } = null!;
}

public class OrderAddressModel
{
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}

public class OrderProductModel
{
    public string ArticleNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string UnitPrice { get; set; } = null!;
    public string Quantity { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
}

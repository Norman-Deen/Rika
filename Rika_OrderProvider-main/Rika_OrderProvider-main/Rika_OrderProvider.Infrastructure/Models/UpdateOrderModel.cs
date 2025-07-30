namespace Rika_OrderProvider.Infrastructure.Models;

public class UpdateOrderModel
{
    public int OrderId { get; set; }
    public string TotalAmount { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string ShipmentMethod { get; set; } = null!;
    public string OrderStatus { get; set; } = null!;
    public UpdateOrderCustomerModel OrderCustomer { get; set; } = null!;
    public UpdateOrderAddressModel OrderAddress { get; set; } = null!;
    public ICollection<UpdateOrderProductModel> OrderProducts { get; set; } = null!;
}

public class UpdateOrderCustomerModel
{
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public string CustomerPhone { get; set; } = null!;
}

public class UpdateOrderAddressModel
{
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}

public class UpdateOrderProductModel
{
    public string ArticleNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string UnitPrice { get; set; } = null!;
    public string Quantity { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
}



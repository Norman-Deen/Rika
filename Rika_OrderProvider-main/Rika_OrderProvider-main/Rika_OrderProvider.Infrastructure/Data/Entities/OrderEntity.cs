using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rika_OrderProvider.Infrastructure.Data.Entities;

public class OrderEntity
{
    [Key]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string TotalAmount { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string ShipmentMethod { get; set; } = null!;
    public string OrderStatus { get; set;  } = "Pending";

    public string OrderCustomerId { get; set; } = null!;
    public OrderCustomerEntity OrderCustomer { get; set; } = null!;

    public string OrderAddressId { get; set; } = null!;
    public OrderAddressEntity OrderAddress { get; set; } = null!;

    public ICollection<OrderProductEntity> OrderProducts { get; set; } = [];

}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rika_OrderProvider.Infrastructure.Data.Entities;

public class OrderCustomerEntity
{
    [Key]
    public string OrderCustomerId { get; set; } = Guid.NewGuid().ToString();
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;


    [JsonIgnore]
    public ICollection<OrderEntity> Orders { get; set; } = [];

}
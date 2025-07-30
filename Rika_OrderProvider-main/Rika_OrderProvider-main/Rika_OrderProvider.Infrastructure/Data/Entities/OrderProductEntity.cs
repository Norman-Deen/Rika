using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rika_OrderProvider.Infrastructure.Data.Entities;


public class OrderProductEntity
{
    public string ArticleNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string UnitPrice { get; set; } = null!;
    public string Quantity { get; set; } = null!;
    public string? Color { get; set; }
    public string? Size { get; set; }

    public int OrderId { get; set; }

    [JsonIgnore]
    public OrderEntity Order { get; set; } = null!;
}
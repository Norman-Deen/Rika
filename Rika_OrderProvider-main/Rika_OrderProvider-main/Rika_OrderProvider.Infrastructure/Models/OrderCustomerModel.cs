using System.ComponentModel.DataAnnotations;


namespace Rika_OrderProvider.Infrastructure.Models;

public class OrderCustomerModel
{
    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    public string CustomerEmail { get; set; } = null!;

    [Required]
    public string CustomerPhone { get; set; } = null!;

}
using System.ComponentModel.DataAnnotations;


namespace Rika_OrderProvider.Infrastructure.Models;

public class OrderAddressModel
{

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string PostalCode { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

}
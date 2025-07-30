using System.ComponentModel.DataAnnotations;

namespace AccountProvider.Entities;

public class UserEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public bool IsEmailConfirmed { get; set; } = false;
    [Required]
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }

    public string? AddressId { get; set; }
}

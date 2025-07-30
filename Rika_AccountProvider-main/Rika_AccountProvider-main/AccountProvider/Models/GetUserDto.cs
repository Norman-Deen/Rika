namespace AccountProvider.Models;

public class GetUserDto
{
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string? Gender { get; set; }

    public int Age { get; set; }
}

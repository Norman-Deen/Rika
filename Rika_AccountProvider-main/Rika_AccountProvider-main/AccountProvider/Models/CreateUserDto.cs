using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountProvider.Models;

public class CreateUserDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }
}

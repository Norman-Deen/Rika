using System.ComponentModel.DataAnnotations;

namespace Rika_WebApp.ViewModels.Profile;

public class ProfileFormViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    [MinLength(2, ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    [MinLength(2, ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email address")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email is invalid")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
    [Required(ErrorMessage = "Invalid phone")]
    [MinLength(5, ErrorMessage = "Invalid phone")]
    public string? Phonenumber { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Upload image", Prompt = "Image url", Order = 4)]
    [MinLength(2, ErrorMessage = "Invalid ProfileImageUrl")]
    public string? ProfileImageUrl { get; set; }

    [Display(Name = "Age", Prompt = "Enter your age", Order = 5)]
    [Required(ErrorMessage = "Invalid age")]
    [MinLength(1, ErrorMessage = "Invalid age")]
    public string? Age { get; set; }
}

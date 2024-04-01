using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels;

public class UserViewModel
{
    [Display(Name = "FullName")]
    public string? FullName { get; set; }   
    
    [Display(Name = "FullName")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "This {0} field is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [Required(ErrorMessage = "This {0} field is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "This {0} field is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }

    public List<string>? Role { get; set; }

    public bool Active { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "This {0} field is required")]
    [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "This {0} field is required")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}

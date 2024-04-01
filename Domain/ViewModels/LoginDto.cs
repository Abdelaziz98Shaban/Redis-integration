using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels;

public class LoginDto
{
    [Required(ErrorMessage = "This {0} field is required")]
    [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]

    public string Password { get; set; }
}

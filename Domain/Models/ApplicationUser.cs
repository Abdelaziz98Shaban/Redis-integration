using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ApplicationUser : IdentityUser<string>
{
    public string FullName { get; set; }
    public bool Active { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryUTC { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
}


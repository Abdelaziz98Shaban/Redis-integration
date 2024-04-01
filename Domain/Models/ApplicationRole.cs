using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole() { }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }

    public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();

}

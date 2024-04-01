using System.Security.Claims;

namespace Data.Services.Interfaces;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
}

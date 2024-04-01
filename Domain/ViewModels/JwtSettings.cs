namespace Domain.ViewModels;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public double AccessTokenExpiry { get; set; }
}

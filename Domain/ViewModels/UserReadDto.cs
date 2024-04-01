namespace Domain.ViewModels;

public class UserReadDto
{
    public string? Id { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
}

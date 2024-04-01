namespace Domain.ViewModels;

public class RedisSettings
{
    public string ConnectionString { get; set; }
    public int ExpirationTimeMinutes { get; set; }

}

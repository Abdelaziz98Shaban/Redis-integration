
namespace Domain.Models;

public class BaseEntity :IModifyable, ICUTracking
{
    public int Id { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOnUtc { get; set; } = DateTime.UtcNow;
    public bool Deleted { get ; set ; }
}

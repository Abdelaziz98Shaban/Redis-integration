namespace Domain.ViewModels;

public class ErrorRequestViewModel
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}
public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
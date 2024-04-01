namespace Domain.ViewModels;

public class IdentityResultViewModel
{
    public IEnumerable<ErrorRequestViewModel> Errors { get; set; }

    public bool Succeeded { get; set; }
    public string Msg { get; set; }
    public UserReadDto Data { get; set; }

}

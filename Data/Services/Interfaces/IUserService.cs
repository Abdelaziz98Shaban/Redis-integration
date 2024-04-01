using Domain.ViewModels;

namespace Data.Services.Interfaces;

public interface IUserService
{
    public Task<IdentityResultViewModel> AddTestAccount(UserViewModel userViewModel, string? currentUserId = null);
    public Task<IdentityResultViewModel> LoginUserAPIAsync(LoginDto model);

}

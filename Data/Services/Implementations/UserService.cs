using AutoMapper;
using Data.Services.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Data.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IMapper mapper, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    public async Task<IdentityResultViewModel> AddTestAccount(UserViewModel userViewModel, string? currentUserId = null)
    {

        var emailExist = await UserEmailExist(userViewModel.Email);
        if (!emailExist.Succeeded)
            return emailExist;



        ApplicationUser userModel = new ApplicationUser
        {
            UserName = userViewModel.UserName,
            FullName = userViewModel.FullName,
            Email = userViewModel.Email,
            Active = true,
            EmailConfirmed = true,
        };

        IdentityResult result = await _userManager.CreateAsync(userModel, userViewModel.Password);

        if (result.Succeeded)
        {

            if (userViewModel.Role != null)
            {
                await ChangeUserRole(userModel.Id, userViewModel.Role.ToList());
            }
        }
        var resultModel = _mapper.Map<IdentityResult, IdentityResultViewModel>(result);
        return resultModel;
    }


    public async Task<IdentityResultViewModel> LoginUserAPIAsync(LoginDto model)
    {
        var identityResultViewModel = new IdentityResultViewModel();
        var errorList = new List<ErrorRequestViewModel>();
        if (string.IsNullOrEmpty(model.Email))
        {
            identityResultViewModel.Msg = "enter_vaild_email.";
            return identityResultViewModel;

        }

        ApplicationUser? user = await _userManager.Users
            .Where(e => e.Active && e.Email == model.Email)
            .Include(e=>e.UserRoles).ThenInclude(e=>e.Role)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (user == null)
        {
            identityResultViewModel.Msg = "user_does_not_exist";
            return identityResultViewModel;
        }

        if (!user.Active)
            return identityResultViewModel;



        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            identityResultViewModel.Msg = "Invalid_login_attempt.";
            return identityResultViewModel;


        }

        var identity = await GetUserToken(user, identityResultViewModel);


        return identity;

    }

    private async Task<IdentityResultViewModel> GetUserToken(ApplicationUser? userModel, IdentityResultViewModel identityResultViewModel)
    {
        if (userModel == null)
        {
            identityResultViewModel.Succeeded = false;
            return identityResultViewModel;
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", userModel.Id),
                new Claim("FullName", userModel?.FullName?? ""),
                new Claim("Email", userModel?.Email?? ""),
                    new Claim(ClaimTypes.Role, userModel.UserRoles.Select(e=>e.Role.Name).FirstOrDefault()),

            };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();


        var _result = new UserReadDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Id = userModel.Id,
            FullName = userModel.FullName,
            Email = userModel.Email,
        };
        if (userModel != null)
        {

            userModel.RefreshToken = refreshToken;
            userModel.RefreshTokenExpiryUTC = DateTime.UtcNow.AddDays(1);
            await _userManager.UpdateAsync(userModel);
        }
        identityResultViewModel.Succeeded = true;
        identityResultViewModel.Data = _result;
        return identityResultViewModel;
    }
    private async Task<(bool, string)> ChangeUserRole(string userId, List<string>? newRoles)
    {
        string? msg;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            msg = "User not found.";
            return (false, msg);
        }

        var userCurrentRoles = await _userManager.GetRolesAsync(user);

        // Check if the user currently has the Admin role
        if (userCurrentRoles.Contains("Admin"))
        {
            // Check if the new roles don't have the Admin role
            if (!newRoles.Contains("Admin"))
            {
                // Check if there's any other user with the Admin role
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                if (adminUsers.Count <= 1)// only current user has the Admin role
                {
                    msg = "Can't remove the last Admin from its role.";
                    return (false, msg);
                }
            }
        }

        // Update user roles
        var result = await _userManager.RemoveFromRolesAsync(user, userCurrentRoles);
        if (!result.Succeeded)
        {
            msg = "Failed to remove user from current roles.";
            return (false, msg);
        }

        var selectedRoles = await _roleManager.Roles.Where(e => newRoles != null && newRoles.Count > 0 && (newRoles.Contains(e.Name))).Select(e => e.Name).ToListAsync();
        if (selectedRoles.Any())
        {
            var resultRoles = await _userManager.AddToRolesAsync(user, selectedRoles);
            msg = !resultRoles.Succeeded ? "Failed to add user to new roles." : "User roles updated successfully.";
            return !resultRoles.Succeeded ? (false, msg) : (true, msg);

        }
        if (newRoles is { Count: > 0 })
        {
            var createRoles = newRoles.Select(roleName => new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            })
                .ToList();

            newRoles = new List<string>();
            foreach (var roleInfo in createRoles)
            {
                if (!await _roleManager.RoleExistsAsync(roleInfo.Name))
                {
                    await _roleManager.CreateAsync(roleInfo);
                    newRoles.Add(roleInfo.Name);
                }
            }

            if (!newRoles.Any())
            {
                msg = "Failed to add user to new roles.";
                return (false, msg);
            }

            var resultRoles = await _userManager.AddToRolesAsync(user, newRoles.ToList());

            msg = !resultRoles.Succeeded ? "Failed to add user to new roles." : "User roles updated successfully.";
            return !resultRoles.Succeeded ? (false, msg) : (true, msg);


        }
        var defaultRole = await _roleManager.Roles.Where(e => e.Name == "User").Select(e => e.Name).FirstOrDefaultAsync();
        if (defaultRole != null)
        {
            var resultRole = await _userManager.AddToRoleAsync(user, defaultRole);
            msg = !resultRole.Succeeded ? "Failed to add user to new roles." : "User roles updated successfully.";
            return !resultRole.Succeeded ? (false, msg) : (true, msg);

        }
        else
        {
            var createRole = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),

                Name = "User",
                NormalizedName = "USER"
            };
            var resultRole = await _roleManager.CreateAsync(createRole);
            if (resultRole.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, createRole.Name);
            }
            msg = !resultRole.Succeeded ? "Failed to add user to new roles." : "User roles updated successfully.";
            return !resultRole.Succeeded ? (false, msg) : (true, msg);

        }

    }


    private async Task<IdentityResultViewModel> UserEmailExist(string email, string? userId = null)
    {
        var identityResultViewModel = new IdentityResultViewModel();
        var errorList = new List<ErrorRequestViewModel>();
        var userMobileIsExist = await _userManager.Users.AsNoTracking().AnyAsync(e =>
            (userId == null || e.Id != userId) && (e.Email.ToLower() == email.ToLower()));

        if (userMobileIsExist)
        {
            identityResultViewModel.Succeeded = false;
            errorList.Add(new ErrorRequestViewModel
            {
                Code = "Email",
                Description = "This email already exist!"
            });
            identityResultViewModel.Errors = errorList;
            return identityResultViewModel;
        }
        return new IdentityResultViewModel() { Succeeded = true };
    }

}

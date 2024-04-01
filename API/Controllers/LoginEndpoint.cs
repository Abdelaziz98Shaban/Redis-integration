using API.Helpers;
using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;


[Route("Login")]
public class LoginEndpoint : EndpointBaseAsync.WithRequest<LoginDto>.WithResult<IActionResult>
{
    readonly IUserService _userService;

    public LoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [CheckModelState]
    public async override Task<IActionResult> HandleAsync([FromBody] LoginDto request, CancellationToken cancellationToken = default)
    {


        var result = await _userService.LoginUserAPIAsync(request);

        return new OkObjectResult(new
        {
            Result = result.Succeeded,
            Msg = result.Msg,
            Data = result.Data
        });
    }

}





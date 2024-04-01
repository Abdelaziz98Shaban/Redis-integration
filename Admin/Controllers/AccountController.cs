using Data.Services.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private readonly ILogger _logger;
    private readonly IUserService _userService;



    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger,
        IUserService userService,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _userService = userService;
        _roleManager = roleManager;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {

        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = await _userManager.Users.Where(e => e.Active && e.Email.ToLower() == model.Email.ToLower()).FirstOrDefaultAsync();
            if (user != null)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    await _userManager.UpdateAsync(user);
                    // await CreateJWT(user);

                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    //return RedirectToAction(nameof(Lockout));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User Not Exist");
                return View(model);
            }

        }
        // If we got this far, something failed, redisplay form
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        return RedirectToAction("Login", "Account");

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser()
    {
        await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "Admin" });
        await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "Customer" });

        var userModel = new UserViewModel
        {
            Email = "admin@roundpixel.com",
            UserName = "admin.1",
            Active = true,
            FullName = "admin",
            Password = "24528179###",
            ConfirmPassword = "24528179###",
            Role = new List<string> { "Admin" }
        };

        await _userService.AddTestAccount(userModel);

        var userModel2 = new UserViewModel
        {
            Email = "Muhammad@roundpixel.com",
            UserName = "Mhammad.1",
            Active = true,
            FullName = "Muhammad Muhammad",
            Password = "Mh1212",
            ConfirmPassword = "Mh1212",
            Role = new List<string> { "Customer" }
        };

        await _userService.AddTestAccount(userModel2);

        var userModel3 = new UserViewModel
        {
            Email = "Ahmad@roundpixel.com",
            UserName = "Ahmad.290",
            Active = true,
            FullName = "Ahmad Ahmad",
            Password = "Ah1212",
            ConfirmPassword = "Ah1212",
            Role = new List<string> { "Customer" }
        };

        await _userService.AddTestAccount(userModel3);

        return RedirectToAction("Login");
    }


}


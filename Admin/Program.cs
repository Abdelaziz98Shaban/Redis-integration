using Data.AutoMapper;
using Data.Contexts;
using Data.Repositories;
using Data.Services.Implementations;
using Data.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Web;

namespace Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddRazorPages()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddCookieTempDataProvider()
                .AddXmlSerializerFormatters();

            builder.Services.AddControllersWithViews()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddCookieTempDataProvider();

            builder.Services.ConfigureApplicationCookie(options => {

                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(3);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Login";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async (ctx) => {
                        ctx.Response.Redirect("/en/account/login?returnUrl=" + HttpUtility.UrlEncode(ctx.Request.Path.ToString()));

                    },
                    OnRedirectToAccessDenied = async (ctx) => {
                        ctx.Response.Redirect("/en/account/login?returnUrl=" + HttpUtility.UrlEncode(ctx.Request.Path.ToString()));
                    }

                };

            });
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Signin settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // User settings
                options.User.RequireUniqueEmail = false;
            })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddTransient<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IUOMService, UOMService>();

            // Add services to the container.

            var app = builder.Build();

            app.UseFileServer();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx => {
                    const int durationInSeconds = 60 * 60 * 24 * 7;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
            name: "dashboard",
            pattern: "{*url}",
            defaults: new { controller = "Home", action = "Index" });

            app.MapRazorPages();

            app.Run();
        }
    }
}

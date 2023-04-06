using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MVC.Models.Account;
using MVC.Repository;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IUserStore<UserAccount>, UserAccountRepository>();
            builder.Services.AddTransient<IRoleStore<UserRole>, UserRoleRepository>();
            builder.Services.AddTransient<IAccountManagerRepository, AccountManagerRepository>();

            builder.Services.AddIdentity<Models.Account.UserAccount, Models.Account.UserRole>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Login";
                config.AccessDeniedPath = "/Account/AccessDenied";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                config.Cookie.Domain = "localhost";
                config.Cookie.HttpOnly = true;
                config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                config.Cookie.Path = "/";
                config.Cookie.Name = "hortagramCookie";
            });

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Login/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
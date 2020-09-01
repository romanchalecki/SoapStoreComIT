using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoapStoreComIT.Areas.Identity.Data;
using SoapStoreComIT.Models;

[assembly: HostingStartup(typeof(SoapStoreComIT.Areas.Identity.IdentityHostingStartup))]
namespace SoapStoreComIT.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SoapStoreComITIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection"))); //context.Configuration.GetConnectionString("SoapStoreComITIdentityDbContextConnection")));


                services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<SoapStoreComITIdentityDbContext>().AddDefaultUI();

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

            });
        }
    }
}

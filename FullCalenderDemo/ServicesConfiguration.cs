using FullCalenderDemo.Areas.Identity.Data;
using FullCalenderDemo.Automapper.Profiles;
using FullCalenderDemo.Data;
using FullCalenderDemo.DBContext;
using FullCalenderDemo.Repos;
using FullCalenderDemo.Repos.Interfaces;
using FullCalenderDemo.Services;
using FullCalenderDemo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo
{
    public static class ServicesConfiguration
    {
        private static IServiceCollection _services { get; set; }
        public static void ConfigureSerices(IServiceCollection services)
        {
            _services = services;
            ConfigureMVC();
            ConfigureDBContext();
            ConfigureIdentity();
            ConfigureDI();
            ConfigurAutoMapper();
        }

        private static void ConfigureMVC()
        {
            _services.AddControllersWithViews();
            _services.AddRazorPages();
        }
        private static void ConfigureIdentity()
        {
            _services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FullCalenderDemoContext>();
            _services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            });
        }
        private static void ConfigureDBContext()
        {
            _services.AddDbContext<CalendarDBCTX>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
        }
        private static void ConfigureDI()
        {
            _services.AddScoped<IEventRepo, EventRepo>();
            _services.AddScoped<IEventService, EventService>();
            _services.AddScoped<ICustomerRepo, CustomerRepo>();
            _services.AddScoped<ICustomerService, CustomerService>();
            _services.AddScoped<IServiceRepo, ServiceRepo>();
            _services.AddScoped<IServiceService, ServiceService>();
            _services.AddScoped<IUserService, UserService>();

        }
        private static void ConfigurAutoMapper()
        {
            _services.AddAutoMapper(c => c.AddProfile<UserProfile>(), typeof(Startup));
        }

    }
}

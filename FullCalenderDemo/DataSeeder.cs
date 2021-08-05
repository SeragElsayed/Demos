using FullCalenderDemo.Areas.Identity.Data;
using FullCalenderDemo.DBContext;
using FullCalenderDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo
{
    public static class DataSeeder
    {
        
        private static string[] _roles { get; set; } = new string[] { "SuperAdmin","Admin","Finance","Sales","Manager","Employee"};
        private static Customer[] _customers { get; set; } = new Customer[] { new Customer { Name="Robotica",Address="Alex"} };
        private static Service[] _services { get; set; } = new Service[] { new Service { Name="IT consultancy"} };
        public static void SeedData(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, CalendarDBCTX calendarDBCTX)
        {
            SeedUsers(userManager);
            SeedRoles(roleManager);
            SeedCustomers( calendarDBCTX);
            SeedServices(calendarDBCTX);
        }
        public static void SeedCustomers(CalendarDBCTX calendarDBCTX)
        {
            var customer = calendarDBCTX.Customers.FirstOrDefault(c => string.IsNullOrEmpty(c.Name)==false);
            if (customer == null)
            {
                calendarDBCTX.Customers.AddRange(_customers);
                calendarDBCTX.SaveChanges();
            }
        }
        public static void SeedServices(CalendarDBCTX calendarDBCTX)
        {
            var service = calendarDBCTX.Services.FirstOrDefault(s => string.IsNullOrEmpty(s.Name) == false);
            if (service == null)
            {
                calendarDBCTX.Services.AddRange(_services);
                calendarDBCTX.SaveChanges();
            }
        }

        public static void SeedUsers (UserManager<ApplicationUser> userManager)
        {
            string email;
            ApplicationUser user;
            foreach (var role in _roles)
            {
                email = $"{role}@gmail.com";
                user =  userManager.FindByNameAsync(email).Result;
                if (user == null)
                {
                    ApplicationUser newUser = new ApplicationUser();
                    newUser.UserName = email;
                    newUser.Email = email;

                    IdentityResult result = userManager.CreateAsync(newUser, "P@ssw0rd").Result;

                    if (result.Succeeded)
                    {
                         userManager.AddToRoleAsync(newUser, role).Wait();
                    }
                }
                else
                {
                  userManager.AddToRoleAsync(user, role).Wait();

                }
            }

        }

        public static void SeedRoles (RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in _roles)
            {
                if (!  roleManager.RoleExistsAsync(role).Result)
                {
                    IdentityRole newRole = new IdentityRole();
                    newRole.Name = role;
                    IdentityResult roleResult =  roleManager.CreateAsync(newRole).Result;
                }
            }
        }
    }
}

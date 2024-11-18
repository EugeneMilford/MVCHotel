using HotelManagement.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Data
{
    public class RoleSeeder
    {
        private readonly UserManager<HotelUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(UserManager<HotelUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync(); // Call to seed users
        }

        private async Task SeedRolesAsync()
        {
            var roles = new List<string> { "User", "Admin" };

            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            // Example admin user
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";

            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new HotelUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Example regular user
            var userEmail = "user@example.com";
            var userPassword = "User@123";

            if (await _userManager.FindByEmailAsync(userEmail) == null)
            {
                var regularUser = new HotelUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "Regular",
                    LastName = "User"
                };

                var result = await _userManager.CreateAsync(regularUser, userPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }
    }
}
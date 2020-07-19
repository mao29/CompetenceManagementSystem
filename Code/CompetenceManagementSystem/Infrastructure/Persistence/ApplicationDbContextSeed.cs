using Application.Common.Constants;
using CompetenceManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceManagementSystem.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AuthorizationConstants.Roles.ADMINISTRATORS))
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATORS));
            }
            
            if (userManager.Users.All(u => u.UserName != AuthorizationConstants.Users.DEMO))
            {
                var defaultUser = new ApplicationUser { UserName = AuthorizationConstants.Users.DEMO };
                await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
            }

            if (userManager.Users.All(u => u.UserName != AuthorizationConstants.Users.ADMIN))
            {
                var adminUserName = AuthorizationConstants.Users.ADMIN;
                var adminUser = new ApplicationUser { UserName = adminUserName };
                await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
                adminUser = await userManager.FindByNameAsync(adminUserName);
                await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATORS);
            }
        }
    }
}

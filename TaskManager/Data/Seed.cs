using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider
            , IConfiguration Configuration)
        {
            //adding custom roles
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Seed database code goes here
                var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string[] roleNames = { "Manager", "Employee" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    //creating the roles and seeding them to the database
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                var Email = "Manager@taskMgr.com";
                var poweruser = new IdentityUser
                {
                    UserName = Email,
                    Email = Email
                };
                string UserPassword = "Mgr@123";
                var _user = await UserManager.FindByEmailAsync(Email);
                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                    if (createPowerUser.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(poweruser, "Manager");
                    }
                }
            }

        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TP_test.Areas.Identity.Data;

namespace TP_test.Database
{
    public static class IdentityManager
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Administrateur", "Utilisateur" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser? user = await UserManager.FindByEmailAsync("root@cstjean.qc.ca");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "root@cstjean.qc.ca",
                    Email = "root@cstjean.qc.ca",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };
                await UserManager.CreateAsync(user, "306P@ssw0rd");
            }
            await UserManager.AddToRoleAsync(user, "Administrateur");
        }
    }
}

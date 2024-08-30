using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Application.Enums;

namespace Identity.Seeds
{
    public static class DefaultRols
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Rols.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Rols.Basic.ToString()));
        }
    }
}

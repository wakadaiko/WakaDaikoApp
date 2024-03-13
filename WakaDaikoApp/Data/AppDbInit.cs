using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WakaDaikoApp.Data
{
    public class AppDbInit{
        public static async Task Seed(AppDbContext db,IServiceProvider provider){
            // Adding Identity userManager object
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager.FindByNameAsync("Admin").Result == null) {
                
                string username = "admin";
                string adminpassword = "AdminPass123!";
                string roleName = "Admin";

                // if role doesn't exist, create it
                await roleManager.CreateAsync(new IdentityRole(roleName));

                // if username doesn't exist, create it and add to role
                if (await userManager.FindByNameAsync(username) == null)
                {
                    AppUser user = new AppUser { UserName = username };
                    var result = await userManager.CreateAsync(user, adminpassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
        }
    }
}

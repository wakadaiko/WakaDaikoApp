using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp
{
    public class AppDbInit
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (context.Events != null && !context.Events.Any())
            {
                // Create - Users

                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                const string ROLE = "Admin";
                const string SECRET_PASSWORD = "Secret!123";
                bool isSuccess = true;

                if (roleManager.FindByNameAsync(ROLE).Result == null) isSuccess = roleManager.CreateAsync(new IdentityRole(ROLE)).Result.Succeeded;

                var user1 = new AppUser { Name = "John Smith", UserName = "John" };
                var user2 = new AppUser { Name = "Jane Doe", UserName = "Jane" };

                isSuccess &= userManager.CreateAsync(user1, SECRET_PASSWORD).Result.Succeeded;

                if (isSuccess) isSuccess &= userManager.AddToRoleAsync(user1, ROLE).Result.Succeeded;

                isSuccess &= userManager.CreateAsync(user2, SECRET_PASSWORD).Result.Succeeded;

                // Create - Events

                if (isSuccess)
                {
                    var randomTitle = "Lorem ipsum";
                    var randomText = "Lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum";
                    var currentTime = DateOnly.FromDateTime(DateTime.Now);

                    for (var i = 0; i < 18; i++)
                    {
                        var event1 = new Event
                        {
                            Title = randomTitle,
                            Text = randomText,
                            Date = currentTime,
                            Author = user2
                        };

                        context.Events.Add(event1);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

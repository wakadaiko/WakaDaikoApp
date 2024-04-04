using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WakaDaikoApp.Data
{
    public class AppDbInit
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (context.Comments != null && !context.Comments.Any())
            {
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

                if (isSuccess)
                {
                    var message1 = new Comment
                    {
                        Title = "Test",
                        Body = "This is a test message",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Author = user1
                    };

                    context.Comments.Add(message1);

                    var message2 = new Comment
                    {
                        Title = "Test",
                        Body = "This is another test message",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Author = user2
                    };

                    context.Comments.Add(message2);
                    context.SaveChanges();
                }
            }
        }
    }
}

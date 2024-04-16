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
                    Random random = new();

                    // Variable - Text
                    const string randomLowerAndUpperChars = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";
                    var randomText = "Lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum";

                    // Test - Search, Date, and Alphabet

                    for (var i = 0; i < 14; i++)
                    {
                        // Variable - Title
                        var randomTitle = new string(Enumerable.Repeat(randomLowerAndUpperChars, 20).Select(s => s[random.Next(s.Length)]).ToArray());

                        // Variable - Date
                        DateTime start = new(1995, 1, 1);

                        int range = (DateTime.Today - start).Days;
                        var currentTime = DateOnly.FromDateTime(start.AddDays(random.Next(range)));

                        // Event
                        var event1 = new Event
                        {
                            Description = randomTitle.ToLower(),
                            Title = randomTitle,
                            Text = randomText,
                            Date = currentTime,
                            Author = user2
                        };

                        context.Events.Add(event1);
                    }

                    // Test - Status

                    var event2 = new Event
                    {
                        Description = "test-status-abc",
                        Title = "Test - Status - aBc",
                        Text = randomText,
                        Date = DateOnly.Parse("3/12/2025"),
                        Author = user2
                    };

                    var event3 = new Event
                    {
                        Description = "test-status-xyz",
                        Title = "Test - Status - XyZ",
                        Text = randomText,
                        Date = DateOnly.Parse("7/3/2026"),
                        Author = user2
                    };

                    // Test - View

                    var event4 = new Event
                    {
                        Description = "test-view-lorem-ipsum",
                        Title = "Test - View - lorem ipsum",
                        Text = randomText,
                        Date = DateOnly.Parse("3/12/2025"),
                        Author = user2
                    };

                    var event5 = new Event
                    {
                        Description = "test-view-symbols",
                        Title = "Test - View - ~!@#$%^&*()",
                        Text = randomText,
                        Date = DateOnly.Parse("7/3/2026"),
                        Author = user2
                    };

                    context.Events.Add(event2);
                    context.Events.Add(event3);
                    context.Events.Add(event4);
                    context.Events.Add(event5);

                    context.SaveChanges();
                }
            }
        }
    }
}

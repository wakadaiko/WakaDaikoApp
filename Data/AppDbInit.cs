using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity;

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
                const string ROLE_SITE_ADMIN = "Site_Admin";
                const string ROLE_TEAM_LEAD = "Team_Lead";
                const string ROLE_TEAM_MEMBER = "Team_Member";
                const string SECRET_PASSWORD = "Secret!123";
                bool isSuccessSiteAdmin = true;
                bool isSuccessTeamLead = true;
                bool isSuccessTeamMember = true;

                if (roleManager.FindByNameAsync(ROLE_SITE_ADMIN).Result == null) isSuccessSiteAdmin = roleManager.CreateAsync(new IdentityRole(ROLE_SITE_ADMIN)).Result.Succeeded;
                if (roleManager.FindByNameAsync(ROLE_TEAM_LEAD).Result == null) isSuccessTeamLead = roleManager.CreateAsync(new IdentityRole(ROLE_TEAM_LEAD)).Result.Succeeded;
                if (roleManager.FindByNameAsync(ROLE_TEAM_MEMBER).Result == null) isSuccessTeamMember = roleManager.CreateAsync(new IdentityRole(ROLE_TEAM_MEMBER)).Result.Succeeded;

                var user1 = new AppUser { Name = "SITE ADMIN", UserName = "SiteAdmin" };
                var user2 = new AppUser { Name = "TEAM LEAD", UserName = "TeamLead" };
                var user3 = new AppUser { Name = "TEAM MEMBER", UserName = "TeamMember" };

                isSuccessSiteAdmin &= userManager.CreateAsync(user1, SECRET_PASSWORD).Result.Succeeded;
                if (isSuccessSiteAdmin) isSuccessSiteAdmin &= userManager.AddToRoleAsync(user1, ROLE_SITE_ADMIN).Result.Succeeded;

                isSuccessTeamLead &= userManager.CreateAsync(user2, SECRET_PASSWORD).Result.Succeeded;
                if (isSuccessTeamLead) isSuccessTeamLead &= userManager.AddToRoleAsync(user2, ROLE_TEAM_LEAD).Result.Succeeded;

                isSuccessTeamMember &= userManager.CreateAsync(user3, SECRET_PASSWORD).Result.Succeeded;
                if (isSuccessTeamMember) isSuccessTeamMember &= userManager.AddToRoleAsync(user3, ROLE_TEAM_MEMBER).Result.Succeeded;

                // Create - Events

                if (isSuccessTeamLead)
                {
                    Random random = new();

                    // Variable - Text
                    const string randomLowerAndUpperChars = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";
                    var randomText = "Lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum, lorem ipsum";

                    // Test - Search, Date, and Alphabet

                    for (int i = 0; i < 50; i++)
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
                        Author = user2,
                        Pinned = true
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

using Xunit;
using WakaDaikoApp.Controllers;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WakaDaikoApp.Tests.Unit
{
    public class UnitTests
    {
        // public readonly AppUser au;

        // public readonly IRepository _r;

        // public readonly AppDbContext _c;

        // public readonly UserManager<AppUser> _um;

        [Fact]
        public void CheckBasic()
        {
            Assert.True(true);
            Assert.False(false);
        }

        [Fact]
        public void CheckCreateUser()
        {
            var repo = new FakeRepository();
            var controller = new AdminController(repo, null!, null!, null!);
            var modelGuardian = new AppUser();
            var modelWard = new AppUser()
            {
                Name = "JohnSmith",
                Instruments = new List<string> { "MainDrums", "BaseBeat" },
                Family = new List<AppUser> { modelGuardian },
                RoleNames = new List<string> { "Team_Member" }
            };

            Assert.True(modelGuardian);
        }
    }
}

using Xunit;
using WakaDaikoApp.Controllers;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Tests.Unit
{
    public class UnitTests
    {
        [Fact]
        public void BasicChecks()
        {
            // Very simple check to ensure unit tests are behaving as expected
            Assert.True(true);
            Assert.False(false);
        }

        // AppUser

        [Fact]
        public void AppUser()
        {
            var modelGuardian = new AppUser()
            {
                Name = "MarthaSmith",
                Instruments = new List<string> { },
                Family = new List<AppUser> { },
                RoleNames = new List<string> { "Parent" }
            };
            var modelWard = new AppUser()
            {
                Name = "CharlieSmith",
                Instruments = new List<string> { "MainDrums" },
                Family = new List<AppUser> { modelGuardian },
                RoleNames = new List<string> { "Team_Member" }
            };

            Assert.NotNull(modelWard);
        }

        // Account

        [Fact]
        public void Account_Register()
        {
            var repo = new FakeRepository();
            var controller = new AccountController(repo, null!, null!, null!);
            var model = new RegisterVM()
            {
                Name = "CharlieSmith",
                Username = "charlie_smith",
                Password = "Secret!123",
                ConfirmPassword = "Secret!123",
            };
            var result = controller.Register(model);

            Assert.NotNull(result);
        }

        [Fact]
        public void Account_Login()
        {
            var repo = new FakeRepository();
            var controller = new AccountController(repo, null!, null!, null!);
            var model = new LoginVM()
            {
                Username = "charlie_smith",
                Password = "Secret!123",
                ReturnUrl = "/",
                RememberMe = false,
            };
            var result = controller.Login(model);

            Assert.NotNull(result);
        }

        // Admin

        [Fact]
        public void Admin_CreateTeam()
        {
            var repo = new FakeRepository();
            var controller = new AdminController(repo, null!, null!, null!);
            var result = controller.CreateTeam(
                "littles", // name
                "emily_jones", // teamLead
                "MainDrums", // Instruments
                "emily_jones, charlie_smith, bobby_johnson" // members
            );

            Assert.NotNull(result);
        }

        [Fact]
        public void Admin_CreateUser()
        {
            var repo = new FakeRepository();
            var controller = new AdminController(repo, null!, null!, null!);
            var model = new AdminVM()
            {
                UserName = "charlie_smith",
                UserPassword = "Secret!123",
                UserEmail = "johnsmith@gmail.com",
                Instruments = "MainDrums",
                Positions = "Back",
                RollNames = "Team_Member",
                Teams = "Littles",
                Dependents = "martha_smith"
            };
            var result = controller.CreateUser(model);

            Assert.NotNull(result);
        }

        // Metronome

        [Fact]
        public void Events_Set()
        {
            var repo = new FakeRepository();
            var controller = new MetronomeController(repo, null!, null!);
            var result = controller.Set(70); // bpmToSave

            Assert.NotNull(result);
        }

        [Fact]
        public void Events_Delete()
        {
            var repo = new FakeRepository();
            var controller = new MetronomeController(repo, null!, null!);
            var result = controller.Delete(70); // bpmToDelete

            Assert.NotNull(result);
        }

        // Events

        [Fact]
        public void Events_GetEvents()
        {
            var repo = new FakeRepository();
            var controller = new EventsController(repo, null!);
            var result = controller.GetEvents(
                 "7", // paginationId
                 "test", // descriptionText
                 "xyz", // search
                 "eStatusPast", // status
                 "eAlphabetAZ", // alphabet
                 "eDateOldest" // date
            );

            Assert.NotNull(result);
        }
    }
}

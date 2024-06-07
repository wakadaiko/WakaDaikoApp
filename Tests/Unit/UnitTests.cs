// using Xunit;
// using WakaDaikoApp.Controllers;
// using WakaDaikoApp.Data;
// using WakaDaikoApp.Models;
// using Microsoft.AspNetCore.Identity;

using System;
using System.Threading.Tasks;
using WakaDaikoApp.Controllers;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace WakaDaikoApp.Tests.Unit
{
    public class UnitTests
    {
        public readonly IRepository _r;

        public readonly AppDbContext _c;

        public readonly UserManager<AppUser> _um;

        public readonly SignInManager<AppUser> _sm;

        [Fact]
        public void CheckBasic()
        {
            Assert.True(true);
            Assert.False(false);
        }

        [Fact]
        public async Task CheckLogin()
        {
            // var controller = new AccountController(_r, _c, _um, _sm);
            // var model = new LoginVM()
            // {
            //     Username = "A",
            //     Password = "Secret!123"
            // };

            // var abc = controller.Login(model);

            // Assert.True(abc);

            // var controller = new AccountController(null, null, _um, _sm);
            var controller = new AccountController(_r, _c, _um, _sm);
            // var model = new LoginVM { Username = "validuser", Password = "validpassword" };
            var model = new LoginVM { Username = "SiteAdmin", Password = "Secret!123" };
            // var model = new LoginVM { Username = "SiteAdmin", Password = "Secret!123" };

            // Act
            var result = await controller.Login(model);

            // Assert
            // var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Assert.Equal("Index", redirectToActionResult.ActionName);
            // Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
    }
}

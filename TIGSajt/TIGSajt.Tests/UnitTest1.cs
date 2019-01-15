using Microsoft.AspNetCore.Mvc;
using System;
using TIGSajt.Controllers;
using Xunit;

namespace TIGSajt.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var controller = new HomeController();
            Assert.IsType<RedirectToActionResult>(controller.Index());
        }
    }
}

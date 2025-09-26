using LMS.Controllers;
using LMS.MiddleWare;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.TestFiles
{
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Get()
        {
            //Arrange
            UserRepo userRepo = new UserRepo();
            var username = "admin";
            var password = "1234";

            //Act
            var user = userRepo.GetUsers(username, password);
            //Assert
            Assert.IsNotNull(user);
        }
        [Test]
        public async Task Valid_APIKey()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["X-API-KEY"] = "123";

            var middleware = new ApiKeyMiddleware(c => Task.CompletedTask,
                   new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "ApiKey", "123" } }).Build());

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public async Task InValid_APIKey()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["X-API-KEY"] = "Key123";

            var middleware = new ApiKeyMiddleware(c => Task.CompletedTask,
                   new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "ApiKey", "123" } }).Build());

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo(401));
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TravixBackend.UserService.Domain.Data.Entities;
using TravixBackend.UserService.Domain.Exceptions;
using Xunit;

namespace TravixBackend.UserService.Domain.Tests.Services.UserServiceTests
{
    public class UserServiceTest
    {
        [Fact]
        public void SignInAsync_InvalidUsername_ReturnsInvalidAuthException()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var bookingService = new TravixBackend.UserService.Domain.Services.UserService(serviceProvider.Object);

            _ = Assert.ThrowsAsync<InvalidAuthException>(() => bookingService.SignInAsync(string.Empty,"asaSsaaASAdasd"));
        }

        [Fact]
        public void SignInAsync_InvalidPassword_ReturnsInvalidAuthExceptionAsync()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var bookingService = new TravixBackend.UserService.Domain.Services.UserService(serviceProvider.Object);

            _ = Assert.ThrowsAsync<InvalidAuthException>(() => bookingService.SignInAsync("asaSsaaASAdasd",string.Empty));
        }

        [Fact]
        public async Task SignInAsync_Signin_ReturnsToken()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var dbContext = GetDBContextMock();
            var mockLogger = new Mock<ILogger<TravixBackend.UserService.Domain.Services.UserService>>();

            serviceProvider.Setup(x => x.GetService(typeof(TravixBackendDBContext))).Returns(dbContext);
            serviceProvider.Setup(x => x.GetService(typeof(ILogger<TravixBackend.UserService.Domain.Services.UserService>))).Returns(mockLogger.Object);

            var user = new User
            {
                Id = 1,
                UserName =  "harsha@gmail.com",
                Password = "40CD896FE1362AA76A0E809ED7606C51782822B9115BD002BA609FF05C377B7A",
                FirstName = "Harsha",
                LastName = "Kavinda"
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new TravixBackend.UserService.Domain.Services.UserService(serviceProvider.Object);

            var token = await userService.SignInAsync(user.UserName, "Intel@123");

            Assert.NotNull(token);
        }

        private static TravixBackendDBContext GetDBContextMock()
        {
            var builder = new DbContextOptionsBuilder<TravixBackendDBContext>();
            var options = builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new TravixBackendDBContext(options);
            return dbContext;
        }
    }
}

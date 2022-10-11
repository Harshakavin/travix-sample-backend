using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TravixBackend.BookingService.Domain.Data.Entities;
using TravixBackend.BookingService.Domain.Dtos;
using Xunit;

namespace TravixBackend.BookingService.Domain.Tests.Services.BookingServiceTests
{
    public class BookingServiceTest
    {
        [Fact]
        public void AddBookingAsync_InvalidUsername_ReturnsUnauthorizedAccessException()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var bookingService = new TravixBackend.BookingService.Domain.Services.BookingService(serviceProvider.Object);

            _ = Assert.ThrowsAsync<TravixBackend.BookingService.Domain.Exceptions.UnauthorizedAccessException>(() => bookingService.AddBookingAsync(string.Empty,new Dtos.BookingDto()));
        }

        [Fact]
        public void GetBookingAsync_InvalidUsername_ReturnsUnauthorizedAccessException()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var bookingService = new TravixBackend.BookingService.Domain.Services.BookingService(serviceProvider.Object);

            _ = Assert.ThrowsAsync<TravixBackend.BookingService.Domain.Exceptions.UnauthorizedAccessException>(() => bookingService.GetBookingAsync(string.Empty, 100));
        }

        [Fact]
        public async Task GetBookingAsync_validUserName_ReturnsSuccessAsync()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var dbContext = GetDBContextMock();
            var mockLogger = new Mock<ILogger<TravixBackend.BookingService.Domain.Services.BookingService>>();

            serviceProvider.Setup(x => x.GetService(typeof(TravixBackendDBContext))).Returns(dbContext);
            serviceProvider.Setup(x => x.GetService(typeof(ILogger<TravixBackend.BookingService.Domain.Services.BookingService>))).Returns(mockLogger.Object);

            var booking = new Booking
            {
                Id = 1,
                Name = "harsha",
                FlightCode = "L0",
                ArrivalTime = "12:00 PM",
                Departing = "15:00 PM",
                From = "UK",
                Group = "Air ways",
                PassportNo = "AB-122329",
                Seat = "45",
                Status = "OPEN",
                To = "USA",
                Way = "ONE-WAY",
                Cost = "200USD",
                Date = "200USD",
                UserId = 1
            };
            dbContext.Bookings.Add(booking);
            dbContext.SaveChanges();

            var bookingService = new TravixBackend.BookingService.Domain.Services.BookingService(serviceProvider.Object);

            var bookings = await bookingService.GetBookingAsync("1", 100);

            Assert.Equal(booking.Name, bookings.First().Name);
            Assert.Equal(booking.FlightCode, bookings.First().FlightCode);
            Assert.Equal(booking.ArrivalTime, bookings.First().ArrivalTime);
            Assert.Equal(booking.Departing, bookings.First().Departing);
            Assert.Equal(booking.From, bookings.First().From);
            Assert.Equal(booking.Group, bookings.First().Group);
            Assert.Equal(booking.PassportNo, bookings.First().PassportNo);
            Assert.Equal(booking.Seat, bookings.First().Seat);
            Assert.Equal(booking.Status, bookings.First().Status);
            Assert.Equal(booking.To, bookings.First().To);
            Assert.Equal(booking.Way, bookings.First().Way);
            Assert.Equal(booking.Cost, bookings.First().Cost);
            Assert.Equal(booking.Date, bookings.First().Date);
        }

        [Fact]
        public async Task AddBookingAsync_validUserName_ReturnsSuccessAsync()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var dbContext = GetDBContextMock();
            var mockLogger = new Mock<ILogger<TravixBackend.BookingService.Domain.Services.BookingService>>();

            serviceProvider.Setup(x => x.GetService(typeof(TravixBackendDBContext))).Returns(dbContext);
            serviceProvider.Setup(x => x.GetService(typeof(ILogger<TravixBackend.BookingService.Domain.Services.BookingService>))).Returns(mockLogger.Object);

            var booking = new BookingDto
            {
                Id = 1,
                Name = "harsha",
                FlightCode = "L0",
                ArrivalTime = "12:00 PM",
                Departing = "15:00 PM",
                From = "UK",
                Group = "Air ways",
                PassportNo = "AB-122329",
                Seat = "45",
                Status = "OPEN",
                To = "USA",
                Way = "ONE-WAY",
                Cost = "200USD",
                Date = "200USD"
            };

            var bookingService = new TravixBackend.BookingService.Domain.Services.BookingService(serviceProvider.Object);

            var bookings = await bookingService.AddBookingAsync("1", booking);

            Assert.Equal(booking.Name, bookings.First().Name);
            Assert.Equal(booking.FlightCode, bookings.First().FlightCode);
            Assert.Equal(booking.ArrivalTime, bookings.First().ArrivalTime);
            Assert.Equal(booking.Departing, bookings.First().Departing);
            Assert.Equal(booking.From, bookings.First().From);
            Assert.Equal(booking.Group, bookings.First().Group);
            Assert.Equal(booking.PassportNo, bookings.First().PassportNo);
            Assert.Equal(booking.Seat, bookings.First().Seat);
            Assert.Equal(booking.Status, bookings.First().Status);
            Assert.Equal(booking.To, bookings.First().To);
            Assert.Equal(booking.Way, bookings.First().Way);
            Assert.Equal(booking.Cost, bookings.First().Cost);
            Assert.Equal(booking.Date, bookings.First().Date);
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

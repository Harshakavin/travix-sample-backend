using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravixBackend.BookingService.Domain.Data.Entities;
using TravixBackend.BookingService.Domain.Dtos;

namespace TravixBackend.BookingService.Domain.Services
{
    public class BookingService : IBookingService
    {

        private readonly TravixBackendDBContext _travixBackendDBContext;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IServiceProvider provider)
        {
            _logger = (ILogger<BookingService>)provider.GetService(typeof(ILogger<BookingService>));
            _travixBackendDBContext = (TravixBackendDBContext)provider.GetService(typeof(TravixBackendDBContext));
        }

        public async Task<List<BookingDto>> GetBookingAsync(string username, int limit)
        {
            var user = ValidUserName(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var defaultSize = 1000;
            var bookings = await _travixBackendDBContext.Bookings.Where(b => b.UserId == user.Id).Take(limit != -1 ? limit: defaultSize).ToListAsync();
            var bookingsData = new List<BookingDto>();

            bookings.ForEach(b => bookingsData.Add(new BookingDto
            {
                Id = b.Id,
                Name = b.Name,
                FlightCode = b.FlightCode,
                ArrivalTime = b.ArrivalTime,
                Departing = b.Departing,
                From = b.From,
                Group = b.Group,
                PassportNo = b.PassportNo,
                Seat = b.Seat,
                Status = b.Status,
                To = b.To,
                Way = b.Way,
                UpdatedDate = b.UpdatedDate.ToShortDateString(),
                CreatedDate = b.CreatedDate.ToShortDateString()
            }));

            return bookingsData;
        }

        public async Task<List<BookingDto>> AddBookingAsync(string username, BookingDto booking)
        {
            var user = ValidUserName(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            await _travixBackendDBContext.Bookings.AddAsync(new Booking
            {
                Id = booking.Id,
                UserId = user.Id,
                Name = booking.Name,
                FlightCode = booking.FlightCode,
                ArrivalTime = booking.ArrivalTime,
                Departing = booking.Departing,
                From = booking.From,
                Group = booking.Group,
                PassportNo = booking.PassportNo,
                Seat = booking.Seat,
                Status = booking.Status,
                To = booking.To,
                Way = booking.Way
            });
            _travixBackendDBContext.SaveChanges();

            return await GetBookingAsync(username, -1);
        }

        private User ValidUserName(string username)
        {
            return username == null ?  null : _travixBackendDBContext.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}

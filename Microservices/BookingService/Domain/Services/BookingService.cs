using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        public async Task<List<BookingDto>> GetBookingAsync(string userId)
        {
            throw new InvalidCastException();
        }
    }
}

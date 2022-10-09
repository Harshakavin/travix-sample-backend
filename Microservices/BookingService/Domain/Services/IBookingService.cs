﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TravixBackend.BookingService.Domain.Dtos;

namespace TravixBackend.BookingService.Domain.Services
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetBookingAsync(string username,int limit);
    }
}

using System;

namespace TravixBackend.BookingService.Domain.Data.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

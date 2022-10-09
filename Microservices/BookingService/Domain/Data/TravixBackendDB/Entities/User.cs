using System;
using System.Collections.Generic;

namespace TravixBackend.BookingService.Domain.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual IEnumerable<Booking> Bookings { get; set; }
    }
}

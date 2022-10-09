
using System;
using TravixBackend.BookingService.Domain.Data.Entities;

namespace TravixBackend.BookingService.Domain.Data.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string FlightCode { get; set; }
        public string PassportNo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Departing { get; set; }
        public string Seat { get; set; }
        public string Way { get; set; }
        public string Group { get; set; }
        public string Status { get; set; }
        public string ArrivalTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public virtual User User { get; set; }
    }
}

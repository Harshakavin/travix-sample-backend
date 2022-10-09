using System;
namespace TravixBackend.BookingService.Domain.Dtos
{
    public class BookingDto
    {
        public long Id { get; set; }
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
        public string Cost { get; set; }
        public string Date { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}

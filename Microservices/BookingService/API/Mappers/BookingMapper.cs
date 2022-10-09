using AutoMapper;
using TravixBackend.BookingService.API.Protos;
using TravixBackend.BookingService.Domain.Dtos;

namespace TravixBackend.BookingService.API.Mappers
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingDto, BookingRequest>().ReverseMap();
        }
    }
}

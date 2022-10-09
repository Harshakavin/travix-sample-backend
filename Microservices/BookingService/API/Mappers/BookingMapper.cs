using AutoMapper;
using TravixBackend.BookingService.Domain.Dtos;

namespace TravixBackend.BookingService.API.Mappers
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingDto, BookingDto>().ReverseMap();
        }
    }
}

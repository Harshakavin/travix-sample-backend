using AutoMapper;
using TravixBackend.BookingService.API.Protos;

namespace TravixBackend.API.Mappers
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingResponse, Dtos.V1.BookingDto>();
        }
    }
}

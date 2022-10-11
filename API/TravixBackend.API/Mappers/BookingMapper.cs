using AutoMapper;
using TravixBackend.BookingService.API.Protos;
using TravixBackend.UserService.API.Protos;

namespace TravixBackend.API.Mappers
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingRequest, Dtos.V1.BookingDto>().ReverseMap();
        }
    }
}

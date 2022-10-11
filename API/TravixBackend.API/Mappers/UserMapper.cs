using AutoMapper;
using TravixBackend.BookingService.API.Protos;
using TravixBackend.UserService.API.Protos;

namespace TravixBackend.API.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<Dtos.V1.SignUpDto, SignUpRequest> ().ReverseMap();
        }
    }
}

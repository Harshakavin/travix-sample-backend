using AutoMapper;
using TravixBackend.UserService.API.Protos;
using TravixBackend.UserService.Domain.Dtos;

namespace TravixBackend.UserService.API.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<SignUpRequest, UserDto>().ReverseMap();
        }
    }
}

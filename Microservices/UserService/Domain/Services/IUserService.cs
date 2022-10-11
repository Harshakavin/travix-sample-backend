using System.Collections.Generic;
using System.Threading.Tasks;
using TravixBackend.UserService.Domain.Dtos;

namespace TravixBackend.UserService.Domain.Services
{
    public interface IUserService
    {
        Task<string> SignInAsync(string userName, string password);
        Task SignUpAsync(UserDto user);
    }
}

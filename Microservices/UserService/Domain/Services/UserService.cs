using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TravixBackend.UserService.Domain.Data.Entities;
using TravixBackend.UserService.Domain.Dtos;
using TravixBackend.UserService.Domain.Exceptions;
using TravixBackend.UserService.Domain.Utils;

namespace TravixBackend.UserService.Domain.Services
{
    public class UserService : IUserService
    {

        private readonly TravixBackendDBContext _travixBackendDBContext;
        private readonly ILogger<UserService> _logger;

        public UserService(IServiceProvider provider)
        {
            _logger = (ILogger<UserService>)provider.GetService(typeof(ILogger<UserService>));
            _travixBackendDBContext = (TravixBackendDBContext)provider.GetService(typeof(TravixBackendDBContext));
        }

        public async Task<string> SignInAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new InvalidAuthException();
            }

            var user = await _travixBackendDBContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == Hash(password));

            if (user == null)
            {
                throw new InvalidAuthException();
            }

            return GenerateJWT(user);
        }

        public async Task SignUpAsync(UserDto user)
        {
            if (!Validator.IsValidEmail(user.UserName) || !Validator.IsValidPassword(user.Password) ||
                string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Phone))
            {
                throw new InvalidRequestException();
            }

            if (HasUserName(user.UserName))
            {
                _logger.LogInformation("User already exisit");
                throw new UserNameAlreadyExsistException();
            }

            var newUser = new User
            {
                UserName = user.UserName,
                Password = Hash(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = true
            };

            await _travixBackendDBContext.Users.AddAsync(newUser);
            _travixBackendDBContext.SaveChanges();
        }

        private string GenerateJWT(User user)
        {
            var credentials = new SigningCredentials(new RsaSecurityKey(new RSACryptoServiceProvider(2048)), SecurityAlgorithms.RsaSha256Signature);

            var JWTHeader = new JwtHeader(credentials);

            var payload = new JwtPayload
            { 
                { "iss", "Travix"},
                { "firstName", $"{user.FirstName}" },
                { "lastName", $"{user.LastName}" },
                { "userName", $"{user.UserName}" },
                { "userId", $"{user.Id}" },
                { "phone", $"{user.Phone}" },
                { "exp", (Int32)(DateTime.UtcNow.AddHours(1).Subtract(new DateTime().AddHours(1))).TotalSeconds},
                { "iat", (Int32)(DateTime.UtcNow.Subtract(new DateTime())).TotalSeconds}
            };

            var token = new JwtSecurityToken(JWTHeader, payload);
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private bool HasUserName(string username)
        {
            return _travixBackendDBContext.Users.Any(u => u.UserName == username);
        }

        public static string Hash(string value) => string.IsNullOrEmpty(value) ? value : SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)).Aggregate(new StringBuilder(), (sb, b) => sb.Append(b.ToString("X2"))).ToString();
    }
}

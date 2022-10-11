using System;

namespace TravixBackend.UserService.Domain.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException()
        {
        }
        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}

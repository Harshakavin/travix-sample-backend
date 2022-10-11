using System;

namespace TravixBackend.UserService.Domain.Exceptions
{
    public class InvalidAuthException : Exception
    {
        public InvalidAuthException()
        {
        }
        public InvalidAuthException(string message) : base(message)
        {
        }
    }
}

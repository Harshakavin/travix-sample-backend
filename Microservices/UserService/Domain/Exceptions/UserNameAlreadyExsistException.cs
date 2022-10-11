using System;

namespace TravixBackend.UserService.Domain.Exceptions
{
    public class UserNameAlreadyExsistException : Exception
    {
        public UserNameAlreadyExsistException()
        {
        }
        public UserNameAlreadyExsistException(string message) : base(message)
        {
        }
    }
}

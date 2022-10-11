

using System.Text.RegularExpressions;

namespace TravixBackend.UserService.Domain.Utils
{
    public static class Validator
    {
        public static bool IsValidEmail(string email) =>
         !string.IsNullOrEmpty(email) && email.Length <= 50 && Regex.IsMatch(email, @"^([a-zA-Z0-9_\-\+\._\u00D8-\u00F6]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$");
        public static bool IsValidPassword(string password) => !string.IsNullOrEmpty(password) && password.Length <= 50;
    }
}

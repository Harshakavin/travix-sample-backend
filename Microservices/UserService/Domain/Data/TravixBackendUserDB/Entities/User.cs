using System;
namespace TravixBackend.UserService.Domain.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

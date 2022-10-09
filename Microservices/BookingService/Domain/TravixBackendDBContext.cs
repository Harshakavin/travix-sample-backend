using TravixBackend.BookingService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravixBackend.BookingService.Domain
{
    public class TravixBackendDBContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public TravixBackendDBContext(DbContextOptions<TravixBackendDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[] {
                new User{UserName="admin",Password="d033e22ae348aeb5660fc2140aec35850c4da997"}
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

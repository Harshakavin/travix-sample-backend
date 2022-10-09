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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

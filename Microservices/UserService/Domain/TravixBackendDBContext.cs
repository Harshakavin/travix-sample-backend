using TravixBackend.UserService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravixBackend.UserService.Domain
{
    public class TravixBackendDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

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

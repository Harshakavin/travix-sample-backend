using TravixBackend.BookingService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravixBackend.BookingService.Domain.Data.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.UserName).HasColumnName("username");
            builder.Property(p => p.Password).HasColumnName("password");
            builder.Property(b => b.CreatedDate).HasColumnName("created_date");
            builder.HasMany(s => s.Bookings).WithOne(s => s.User).HasForeignKey(s => s.UserId);
            builder.HasKey(b => b.Id);
        }
    }
}

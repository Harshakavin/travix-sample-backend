using TravixBackend.BookingService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravixBackend.BookingService.Domain.Data.EntityConfigurations
{
    public class BookingEntityConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Name).HasColumnName("name");
            builder.Property(p => p.PassportNo).HasColumnName("passportNo");
            builder.Property(p => p.FlightCode).HasColumnName("flightCode");
            builder.Property(p => p.Group).HasColumnName("group");
            builder.Property(p => p.From).HasColumnName("from");
            builder.Property(p => p.To).HasColumnName("to");
            builder.Property(p => p.Departing).HasColumnName("departingAt");
            builder.Property(p => p.Seat).HasColumnName("seat");
            builder.Property(p => p.Way).HasColumnName("way");
            builder.Property(p => p.Group).HasColumnName("group");
            builder.Property(b => b.Status).HasColumnName("status");
            builder.Property(b => b.ArrivalTime).HasColumnName("arrivalTime");
            builder.Property(b => b.Date).HasColumnName("date");
            builder.Property(b => b.Cost).HasColumnName("cost");
            builder.Property(b => b.CreatedDate).HasColumnName("created_date");
            builder.Property(b => b.UpdatedDate).HasColumnName("updated_date");
            //builder.HasOne(s => s.User).WithMany(s => s.Bookings).HasForeignKey(s => s.UserId);
            builder.HasKey(b => b.Id);
        }
    }
}
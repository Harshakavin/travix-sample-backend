using TravixBackend.UserService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravixBackend.UserService.Domain.Data.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.FirstName).HasColumnName("first_name");
            builder.Property(p => p.LastName).HasColumnName("last_name");
            builder.Property(p => p.Phone).HasColumnName("phone");
            builder.Property(p => p.UserName).HasColumnName("username");
            builder.Property(p => p.Password).HasColumnName("password");
            builder.Property(p => p.Status).HasColumnName("status");
            builder.Property(b => b.CreatedDate).HasColumnName("created_date");
            builder.HasKey(b => b.Id);
        }
    }
}

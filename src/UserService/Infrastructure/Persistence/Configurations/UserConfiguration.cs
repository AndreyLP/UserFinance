using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                  .HasColumnName("id");

            builder.Property(u => u.Name)
                  .HasColumnName("name")
                  .IsRequired();

            builder.Property(u => u.Password)
                  .HasColumnName("password")
                  .IsRequired();

            builder.HasIndex(u => u.Name)
                  .IsUnique();
        }
    }
}

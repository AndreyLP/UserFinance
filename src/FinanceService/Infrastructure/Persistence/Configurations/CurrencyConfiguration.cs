using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure.Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currency");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Rate)
                .HasColumnName("rate")
                .HasColumnType("numeric")
                .IsRequired(false);

            builder.HasMany(x => x.Favorites)
                   .WithOne(x => x.Currency)
                   .HasForeignKey(x => x.CurrencyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure.Persistence.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("favorites");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(x=>x.UserId)
                .HasColumnName("userId");

            builder.Property(x => x.CurrencyId)
                .HasColumnName("currencyId");

            builder.HasOne(x => x.Currency)
                   .WithMany(x => x.Favorites)
                   .HasForeignKey(x => x.CurrencyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

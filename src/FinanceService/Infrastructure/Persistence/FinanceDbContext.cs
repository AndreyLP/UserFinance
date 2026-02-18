using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Persistence
{
    public class FinanceDbContext : DbContext
    {
        public const string Schema = "finance_service";
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<Favorite> UserFavorites => Set<Favorite>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

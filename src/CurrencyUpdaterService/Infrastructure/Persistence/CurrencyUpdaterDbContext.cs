using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CurrencyUpdaterDbContext : DbContext
    {
        public const string Schema = "finance_service";
        public CurrencyUpdaterDbContext(DbContextOptions<CurrencyUpdaterDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurrencyUpdaterDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Currency> Currencies => Set<Currency>();
    }

    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Rate { get; set; }
    }
}

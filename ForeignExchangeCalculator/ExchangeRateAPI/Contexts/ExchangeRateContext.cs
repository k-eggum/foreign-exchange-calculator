using ExchangeRateAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRateAPI.Contexts
{
    public class ExchangeRateContext: DbContext
    {
        public ExchangeRateContext(DbContextOptions<ExchangeRateContext> options): base(options)
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).ModifiedDateTime = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

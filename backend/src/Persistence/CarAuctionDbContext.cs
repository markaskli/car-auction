using Microsoft.EntityFrameworkCore;

namespace CarAuction.Api.Persistence
{
    public class CarAuctionDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarAuctionDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarAuctionDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

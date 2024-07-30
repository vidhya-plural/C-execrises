using Microsoft.EntityFrameworkCore;
using Dealership.Models;

namespace Dealership.Data
{
    public class DealershipContext : DbContext
    {
        public DealershipContext(DbContextOptions<DealershipContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Car)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict); // Ensure correct delete behavior

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using CumulativeCountReport.Models;

namespace CumulativeCountReport.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WaferCountHistory> WaferCountHistories { get; set; } = null!;
        public DbSet<Equipment> Equipments { get; set; } = null!;
        public DbSet<DoopControlValue> DoopControlValues { get; set; } = null!;
        public DbSet<WaferCountQueryResult> WaferCountQueryResults { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WaferCountHistory>(entity =>
            {
                entity.HasIndex(e => new { e.EopId, e.TestOpNo, e.Date, e.ItemPrompt });
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.EopId);
            });

            modelBuilder.Entity<DoopControlValue>(entity =>
            {
                entity.HasIndex(e => new { e.EopId, e.TestOpNo, e.ItemPrompt });
            });

            modelBuilder.Entity<WaferCountQueryResult>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}

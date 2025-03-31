using Microsoft.EntityFrameworkCore;

namespace SimpleDataApi.Storage
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<CodeValue> CodeValues { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeValue>(entity =>
            {
                entity.HasKey(e => e.Id)
                .IsClustered();

                entity.HasIndex(e => e.Code)
                .IsClustered(false);

                entity.HasIndex(e => e.Value)
                .IsClustered(false);
            });
        }
    }
}

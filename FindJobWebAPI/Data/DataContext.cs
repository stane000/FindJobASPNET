using FindJobWebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace FindJobWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); //Job can only exist in Company


            modelBuilder.Entity<Company>()
                       .HasMany(c => c.Workers)
                       .WithOne(w => w.Company)
                       .HasForeignKey(w => w.CompanyId);
            // Other configurations...
        }



    }
}

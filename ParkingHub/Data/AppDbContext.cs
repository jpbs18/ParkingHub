using Microsoft.EntityFrameworkCore;
using ParkingHub.Data.Entities;


namespace ParkingHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Park> Parks { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyParkQuota> CompanyParkQuotas { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LicensePlate> LicensePlates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.CurrentPark)
                .WithMany()
                .HasForeignKey(e => e.CurrentParkId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CompanyParkQuota>()
                .HasOne(q => q.Company)
                .WithMany(c => c.CompanyParkQuotas)
                .HasForeignKey(q => q.CompanyId);

            modelBuilder.Entity<CompanyParkQuota>()
                .HasOne(q => q.Park)
                .WithMany(p => p.CompanyParkQuotas)
                .HasForeignKey(q => q.ParkId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ParkingHub.Data;

namespace ParkingHub.Extensions
{
    public static class DatabaseExtensions
    {
        public static void ApplyMigrationsAndSeed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                db.Database.Migrate();
                Console.WriteLine("Database migrated successfully!");

                DbSeeder.Seed(db);
                Console.WriteLine("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database migration/seed failed: {ex.Message}");
            }
        }
    }
}

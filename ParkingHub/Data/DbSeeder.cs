using ParkingHub.Data.Entities;

namespace ParkingHub.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {         
            if (context.Companies.Any()) return;

            var companies = new List<Company>
            {
                new() { Name = "TechCorp" },
                new() { Name = "EcoSolutions" },
                new() { Name = "UrbanLogistics" }
            };

            context.Companies.AddRange(companies);
            context.SaveChanges();

            var parks = new List<Park>
            {
                new() { Name = "Central Park", Capacity = 100, OccupiedCount = 0 },
                new() { Name = "North Park", Capacity = 50, OccupiedCount = 0 },
                new() { Name = "South Park", Capacity = 75, OccupiedCount = 0 }
            };

            context.Parks.AddRange(parks);
            context.SaveChanges();

            var quotas = new List<CompanyParkQuota>
            {
                new() { CompanyId = companies[0].Id, ParkId = parks[0].Id, MaxSpots = 30, OccupiedCount = 0 },
                new() { CompanyId = companies[1].Id, ParkId = parks[1].Id, MaxSpots = 20, OccupiedCount = 0 },
                new() { CompanyId = companies[2].Id, ParkId = parks[2].Id, MaxSpots = 25, OccupiedCount = 0 }
            };

            context.CompanyParkQuotas.AddRange(quotas);
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new() { Name = "Alice", Email = "Alice@hotmail.com", CompanyId = companies[0].Id, InsidePark = false, CreatedAt = DateTime.UtcNow },
                new() { Name = "Bob", Email = "Bob@hotmail.com", CompanyId = companies[1].Id, InsidePark = false, CreatedAt = DateTime.UtcNow },
                new() { Name = "Charlie", Email = "Charlie@hotmail.com", CompanyId = companies[2].Id, InsidePark = false, CreatedAt = DateTime.UtcNow }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var plates = new List<LicensePlate>
            {
                new() { EmployeeId = employees[0].Id, PlateNumber = "AA-11-BB" },
                new() { EmployeeId = employees[1].Id, PlateNumber = "CC-22-DD" },
                new() { EmployeeId = employees[2].Id, PlateNumber = "EE-33-FF" }
            };

            context.LicensePlates.AddRange(plates);
            context.SaveChanges();

            Console.WriteLine("Database seeded successfully!");
        }
    }
}

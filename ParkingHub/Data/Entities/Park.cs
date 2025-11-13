using System.ComponentModel.DataAnnotations;

namespace ParkingHub.Data.Entities
{
    public class Park
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int OccupiedCount { get; set; }

        public ICollection<CompanyParkQuota> CompanyParkQuotas { get; set; } = [];
        public ICollection<Employee> Employees { get; set; } = [];
    }
}

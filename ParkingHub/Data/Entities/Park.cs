using System.ComponentModel.DataAnnotations;

namespace ParkingHub.Data.Entities
{
    public class Park
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 1000)]
        public int Capacity { get; set; }

        [Range(0, 1000)]
        public int OccupiedCount { get; set; }

        // Relationships
        public ICollection<CompanyParkQuota> CompanyParkQuotas { get; set; } = [];
        public ICollection<Employee> Employees { get; set; } = [];
    }
}

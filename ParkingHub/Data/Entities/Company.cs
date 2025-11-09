using System.ComponentModel.DataAnnotations;

namespace ParkingHub.Data.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relationships
        public ICollection<Employee> Employees { get; set; } = [];
        public ICollection<CompanyParkQuota> CompanyParkQuotas { get; set; } = [];
    }
}

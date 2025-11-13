using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingHub.Data.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public bool InsidePark { get; set; }
        public int? CurrentParkId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        [ForeignKey(nameof(CurrentParkId))]
        public Park? CurrentPark { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;
        public List<LicensePlate> LicensePlates { get; set; } = [];
    }
}

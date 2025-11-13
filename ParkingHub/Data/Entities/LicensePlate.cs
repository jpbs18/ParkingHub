using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingHub.Data.Entities
{
    public class LicensePlate
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string PlateNumber { get; set; } = string.Empty;

        //Relationships
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}

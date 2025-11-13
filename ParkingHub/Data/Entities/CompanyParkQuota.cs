using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingHub.Data.Entities
{
    public class CompanyParkQuota
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ParkId { get; set; }
        public int MaxSpots { get; set; }
        public int OccupiedCount { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;
        [ForeignKey(nameof(ParkId))]
        public Park Park { get; set; } = null!;
    }
}

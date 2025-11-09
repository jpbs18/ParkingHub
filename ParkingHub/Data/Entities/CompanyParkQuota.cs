using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingHub.Data.Entities
{
    public class CompanyParkQuota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int ParkId { get; set; }

        [Range(1, 1000)]
        public int MaxSpots { get; set; }

        [Range(0, 1000)]
        public int OccupiedCount { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        [ForeignKey(nameof(ParkId))]
        public Park Park { get; set; } = null!;
    }
}

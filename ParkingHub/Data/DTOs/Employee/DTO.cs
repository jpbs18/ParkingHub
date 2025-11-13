using ParkingHub.Data.DTOs.LicensePlate;

namespace ParkingHub.Data.DTOs.Employee
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool InsidePark { get; set; }
        public int? CurrentParkId { get; set; }
        public string? CurrentParkName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<LicensePlateDto> LicensePlates { get; set; } = [];
    }

    public class EmployeeCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public List<string> LicensePlates { get; set; } = [];
    }

    public class EmployeeUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool InsidePark { get; set; }
        public int? CurrentParkId { get; set; }
        public List<string> LicensePlates { get; set; } = [];
    }
}

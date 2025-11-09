using ParkingHub.Data.DTOs.Employee;
using ParkingHub.Data.DTOs.LicensePlate;
using ParkingHub.Data.Entities;

namespace ParkingHub.Data
{
    public static class EmployeeMappings
    {
        public static EmployeeReadDto ToReadDto(this Employee employee)
        {
            return new EmployeeReadDto
            {
                Id = employee.Id,
                Name = employee.Name,
                InsidePark = employee.InsidePark,
                CurrentParkId = employee.CurrentParkId,
                CurrentParkName = employee.CurrentPark?.Name,
                CompanyId = employee.CompanyId,
                CompanyName = employee.Company.Name,
                CreatedAt = employee.CreatedAt,
                LicensePlates = [.. employee.LicensePlates
                    .Select(lp => new LicensePlateDto
                    {
                        Id = lp.Id,
                        PlateNumber = lp.PlateNumber
                    })]
            };
        }

        public static Employee ToEntity(this EmployeeCreateDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                CompanyId = dto.CompanyId,
                CreatedAt = DateTime.UtcNow,
                LicensePlates = [.. dto.LicensePlates.Select(lp => new LicensePlate
                {
                    PlateNumber = lp
                })]
            };
        }

        public static void ApplyUpdate(this Employee employee, EmployeeUpdateDto dto)
        {
            employee.Name = dto.Name;
            employee.InsidePark = dto.InsidePark;
            employee.CurrentParkId = dto.CurrentParkId;
            employee.LicensePlates.Clear();
            employee.LicensePlates.AddRange(dto.LicensePlates.Select(lp => new LicensePlate
            {
                PlateNumber = lp,
                EmployeeId = employee.Id
            }));
        }
    }
}

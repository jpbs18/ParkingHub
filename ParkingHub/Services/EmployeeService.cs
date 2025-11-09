using ParkingHub.Data;
using ParkingHub.Data.DTOs.Employee;
using ParkingHub.Exceptions;
using ParkingHub.Repositories;

namespace ParkingHub.Services
{
    public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeReadDto>> GetAllAsync()
        {
            var employees = await repository.GetAllAsync();
            return employees.Select(e => e.ToReadDto());
        }

        public async Task<EmployeeReadDto?> GetByIdAsync(int id)
        {
            var employee = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Employee with id {id} not found.");

            return employee.ToReadDto();
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto)
        {
            var employee = dto.ToEntity();
            await repository.AddAsync(employee);

            var created = await repository.GetByIdAsync(employee.Id)
                ?? throw new RepositoryException("Failed to load the newly created employee from the database.");

            return created.ToReadDto();
        }

        public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var employee = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Employee with id {id} not found.");
            employee.ApplyUpdate(dto);

            await repository.UpdateAsync(employee);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _ = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Employee with id {id} not found.");

            await repository.DeleteAsync(id);
            return true;
        }
    }
}

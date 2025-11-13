using FluentValidation;
using ParkingHub.Data;
using ParkingHub.Data.DTOs.Employee;
using ParkingHub.Exceptions;
using ParkingHub.Repositories;

namespace ParkingHub.Services
{
    public class EmployeeService(
        IEmployeeRepository repository, 
        IValidator<EmployeeCreateDto> createValidator,
        IValidator<EmployeeUpdateDto> updateValidator
    ) : IEmployeeService
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
            await createValidator.ValidateAndThrowAsync(dto);
            if (await repository.EmailExists(dto.Email))
            {
                throw new ValidationException("An employee with the given email already exists.");
            }

            var employee = dto.ToEntity();
            await repository.AddAsync(employee);

            return employee.ToReadDto();
        }

        public async Task UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            await updateValidator.ValidateAndThrowAsync(dto);
            var employee = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Employee with id {id} not found.");

            if (await repository.EmailExistsForAnotherEmployee(id, dto.Email))
            {
                throw new ValidationException("Email already exists.");
            }

            employee.ApplyUpdate(dto);
            await repository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Employee with id {id} not found.");

            await repository.DeleteAsync(employee);
        }
    }
}

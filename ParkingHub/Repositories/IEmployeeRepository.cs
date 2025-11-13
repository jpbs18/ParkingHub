using ParkingHub.Data.Entities;

namespace ParkingHub.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> EmailExists(string email);
        Task<bool> EmailExistsForAnotherEmployee(int employeeId, string email);
    }
}

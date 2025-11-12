using Microsoft.EntityFrameworkCore;
using ParkingHub.Data;
using ParkingHub.Data.Entities;
using ParkingHub.Exceptions;

namespace ParkingHub.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await context.Employees
                .Include(e => e.Company)
                .Include(e => e.CurrentPark)
                .Include(e => e.LicensePlates)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await context.Employees
                .Include(e => e.Company)
                .Include(e => e.CurrentPark)
                .Include(e => e.LicensePlates)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            try
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding employee to the database", ex);
            }     
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Error updating employee. Concurrency conflict.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating employee in the database.", ex);
            }
        }

        public async Task DeleteAsync(Employee employee)
        {
            try
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error deleting employee from the database.", ex);
            }
        }
    }
}

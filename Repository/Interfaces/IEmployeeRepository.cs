using EmployeesApi.DTOs;
using EmployeesApi.Models;

namespace EmployeesApi.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<Employee>> GetAllAsync(EmployeeSearch? search = null, string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10);
        Task<int> GetCountAsync(string? search = null, string? department = null);
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}

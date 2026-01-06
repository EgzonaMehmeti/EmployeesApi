using EmployeesApi.Models;

namespace EmployeesApi.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<PagedResult<Department>> GetAllAsync(string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10);
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task<Department?> UpdateAsync(Department department);
        Task DeleteAsync(int id);
    }
}

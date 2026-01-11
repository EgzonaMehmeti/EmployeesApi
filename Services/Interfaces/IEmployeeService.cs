using EmployeesApi.DTOs;

namespace EmployeesApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResultDto<EmployeeDto>> GetAllEmployeesAsync(EmployeeSearchDto? search = null, string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}

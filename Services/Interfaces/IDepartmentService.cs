using EmployeesApi.DTOs;

public interface IDepartmentService
{
    Task<PagedResultDto<DepartmentDto>> GetAllDepartmentsAsync(string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10);
    Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
}

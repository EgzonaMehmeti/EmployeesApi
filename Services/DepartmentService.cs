using AutoMapper;
using EmployeesApi.DTOs;
using EmployeesApi.Models;
using EmployeesApi.Repository;
using EmployeesApi.Repository.Interfaces;
using EmployeesApi.Services.Interfaces;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<DepartmentDto>> GetAllDepartmentsAsync(string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10)
    {
        var departments = await _departmentRepository.GetAllAsync(sortBy, sortDir, page, pageSize);

        return new PagedResultDto<DepartmentDto>
        {
            Items = departments.Items.Select(e => _mapper.Map<DepartmentDto>(e)),
            TotalCount = departments.TotalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
            return null;

        return _mapper.Map<DepartmentDto>(department);
    }
}

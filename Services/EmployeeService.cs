using AutoMapper;
using EmployeesApi.DTOs;
using EmployeesApi.Models;
using EmployeesApi.Repository.Interfaces;
using EmployeesApi.Services.Interfaces;

namespace EmployeesApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IDepartmentService departmentService)
        {
            _employeeRepository = employeeRepository;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<EmployeeDto>> GetAllEmployeesAsync(EmployeeSearchDto? search = null, string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10)
        {
            var employeeSearch = _mapper.Map<Models.EmployeeSearch>(search);

            var employees = await _employeeRepository.GetAllAsync(employeeSearch, sortBy, sortDir, page, pageSize);


            return new PagedResultDto<EmployeeDto>
            {
                Items = _mapper.Map<List<EmployeeDto>>(employees.Items),
                TotalCount = employees.TotalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(dto.DepartmentId);
            
            if (department == null)
                throw new ArgumentException("Invalid department ID");

            var employee = _mapper.Map<Models.Employee>(dto);

            employee.DepartmentId = dto.DepartmentId;

            await _employeeRepository.AddAsync(employee);

            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return false;

            var departmentExists = await _departmentService.GetDepartmentByIdAsync(dto.DepartmentId);

            if (departmentExists == null)
                throw new ArgumentException("Invalid department ID");

            _mapper.Map(dto, employee);

            employee.DepartmentId = dto.DepartmentId;

            await _employeeRepository.UpdateAsync(employee);

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var exists = await _employeeRepository.GetByIdAsync(id);
            if (exists == null)
                return false;

            await _employeeRepository.DeleteAsync(id);
            return true;
        }
    }
}

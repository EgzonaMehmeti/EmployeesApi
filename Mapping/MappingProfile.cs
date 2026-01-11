using AutoMapper;
using EmployeesApi.DTOs;
using EmployeesApi.Models;

namespace EmployeesApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<EmployeeSearch, EmployeeSearchDto>();
            CreateMap<EmployeeSearchDto, EmployeeSearch>();

            CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>));
        }
    }
}

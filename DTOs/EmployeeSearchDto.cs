namespace EmployeesApi.DTOs
{
    public class EmployeeSearchDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }

        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }

        public int? DepartmentId { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

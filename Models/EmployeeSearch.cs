namespace EmployeesApi.Models
{
    public class EmployeeSearch
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }
    }
}

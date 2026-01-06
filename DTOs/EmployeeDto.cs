
namespace EmployeesApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public required DepartmentDto Department { get; set; }
        public required string Role { get; set; }
        public required string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }
}

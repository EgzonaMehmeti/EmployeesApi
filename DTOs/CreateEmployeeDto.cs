using EmployeesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.DTOs
{
    public class CreateEmployeeDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required int Age { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public required int DepartmentId { get; set; }
        public required string Role { get; set; }
        public required string Phone { get; set; }
        public required DateTime HireDate { get; set; }
        public required decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }
}

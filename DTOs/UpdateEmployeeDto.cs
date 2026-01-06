using EmployeesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.DTOs
{
    public class UpdateEmployeeDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? Role { get; set; }
        public string? Phone { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public bool? IsActive { get; set; }
    }
}

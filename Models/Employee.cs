using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int Age { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
        public required Department Department { get; set; } = null!;
        public required string Role { get; set; }
        public required string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }
}

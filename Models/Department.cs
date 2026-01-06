using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}

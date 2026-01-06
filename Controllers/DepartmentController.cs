using EmployeesApi.DTOs;
using EmployeesApi.Models;
using Microsoft.AspNetCore.Mvc;

[Route("v1/employee/department")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentsController(IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetAll(
        [FromQuery] string? sortBy = "Id",
        [FromQuery] string? sortDir = "asc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 100) pageSize = 10;

        var response = await _service.GetAllDepartmentsAsync(sortBy, sortDir, page, pageSize);

        return Ok(response);
    }
    // GET: api/employees/department/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _service.GetDepartmentByIdAsync(id);
        if (response == null) return NotFound(new { message = "Department not found." });
        return Ok(response);
    }
}

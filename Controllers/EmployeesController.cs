using AutoMapper;
using EmployeesApi.DTOs;
using EmployeesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApi.Controllers
{
    [Route("v1/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] EmployeeSearchDto? search = null,
            [FromQuery] string? sortBy = "Id",
            [FromQuery] string? sortDir = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var response = await _service.GetAllEmployeesAsync(search, sortBy, sortDir, page, pageSize);

            return Ok(response);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var response = await _service.GetEmployeeByIdAsync(id);
            if (response == null) return NotFound(new { message = "Employee not found." });
            return Ok(response);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] CreateEmployeeDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _service.CreateEmployeeAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            [FromBody] UpdateEmployeeDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _service.UpdateEmployeeAsync(id, updateDto);
                if (!response) return NotFound(new { message = "Employee not found" });
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var response = await _service.DeleteEmployeeAsync(id);
                if (!response) return NotFound(new { message = "Employee not found" });
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

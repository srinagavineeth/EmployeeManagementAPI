using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // PATCH: api/employee/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchEmployee(int id, [FromBody] EmployeeManagementAPI.Models.UpdateEmployeeDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var (success, errors) = await _service.PatchAsync(id, dto);
            if (!success)
            {
                if (errors != null && errors.Contains("NotFound"))
                {
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                if (errors != null && errors.Contains("ConcurrencyError"))
                {
                    return StatusCode(500, "Concurrency error occurred");
                }

                if (errors != null && errors.Contains("Employee with this email already exists"))
                {
                    return Conflict(new { message = "Employee with this email already exists" });
                }

                return BadRequest(new { errors });
            }

            return NoContent();
        }

        // GET: api/employee
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _service.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching employees: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _service.GetByIdAsync(id);

                if (employee == null)
                {
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching employee: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/employee
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> PostEmployee(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // pre-check by email
                var (success, message, employee) = await _service.CreateAsync(createEmployeeDto);
                if (!success)
                {
                    return Conflict(new { message });
                }

                return CreatedAtAction("GetEmployee", new { id = employee!.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating employee: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutEmployee(int id, CreateEmployeeDto updateEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            var (success, error) = await _service.UpdateAsync(id, updateEmployeeDto);
            if (!success)
            {
                if (error == null)
                    return NotFound(new { message = $"Employee with id {id} not found" });
                return Conflict(new { message = error });
            }
            return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error: {ex.Message}");
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound(new { message = $"Employee with id {id} not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting employee: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool EmployeeExists(int id)
        {
            // keep for compatibility; ideally move to service/repository
            return false;
        }
    }
}
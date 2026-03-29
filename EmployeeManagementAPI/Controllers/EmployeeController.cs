using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize > 50 ? 50 : pageSize;

            var employees = await _service.GetAllAsync(pageNumber, pageSize);
            var totalCount = await _service.GetTotalCountAsync();

            return Ok(new ApiResponse<IEnumerable<Employee>>
            {
                Success = true,
                Message = "Employees fetched successfully",
                Data = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            });
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Employee with id {id} not found"
                });
            }

            return Ok(new ApiResponse<Employee>
            {
                Success = true,
                Message = "Employee fetched successfully",
                Data = employee
            });
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> PostEmployee(CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState
                });
            }

            var (success, message, employee) = await _service.CreateAsync(dto);

            if (!success)
            {
                return Conflict(new ApiResponse<string>
                {
                    Success = false,
                    Message = message
                });
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employee!.EmployeeId },
                new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Employee created successfully",
                    Data = employee
                });
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState
                });
            }

            var (success, error) = await _service.UpdateAsync(id, dto);

            if (!success)
            {
                if (error == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Employee with id {id} not found"
                    });
                }

                return Conflict(new ApiResponse<string>
                {
                    Success = false,
                    Message = error
                });
            }

            return NoContent();
        }

        // PATCH: api/employee/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(int id, UpdateEmployeeDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            var (success, errors) = await _service.PatchAsync(id, dto);

            if (!success)
            {
                if (errors != null && errors.Contains("NotFound"))
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Employee with id {id} not found"
                    });
                }

                if (errors != null && errors.Contains("Employee with this email already exists"))
                {
                    return Conflict(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Employee with this email already exists"
                    });
                }

                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Update failed",
                    Errors = errors
                });
            }

            return NoContent();
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Employee with id {id} not found"
                });
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Keyword is required"
                });
            }
            var employees = await _service.SearchAsync(keyword);

            return Ok(new ApiResponse<IEnumerable<Employee>>
            {
                Success = true,
                Message = "Search results",
                Data = employees
            });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByDepartment(string department,int pageNumber = 1,int pageSize = 10)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize > 50 ? 50 : pageSize;
            var employees = await _service.FilterByDepartmentAsync(department, pageNumber,pageSize);

            return Ok(new ApiResponse<IEnumerable<Employee>>
            {
                Success = true,
                Message = "Filtered employees",
                Data = employees,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }
    }
}
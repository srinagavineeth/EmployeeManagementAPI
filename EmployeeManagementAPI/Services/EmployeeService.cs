using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository repository, ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<List<Employee>> GetAllAsync(int pageNumber, int pageSize)
        {
            return _repository.GetAllAsync(pageNumber, pageSize);
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string? Message, Employee? Employee)> CreateAsync(CreateEmployeeDto dto)
        {
            // check duplicate by email
            var existing = await _repository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                return (false, "Employee with this email already exists", null);
            }

            var employee = new Employee
            {
                Name = dto.Name,
                Department = dto.Department,
                Salary = dto.Salary,
                Email = dto.Email,
                CreatedDate = DateTime.UtcNow
            };

           try
            {
                await _repository.AddAsync(employee);
                return (true, null, employee);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Duplicate email error for {Email}", dto.Email);

                return (false, "Employee with this email already exists", null);
            }

            }

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return (false, null);

            employee.Name = dto.Name;
            employee.Department = dto.Department;
            employee.Salary = dto.Salary;
            employee.Email = dto.Email;

            try
            {
                await _repository.UpdateAsync(employee);
                return (true, null);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException while updating employee id {EmployeeId} with email {Email}", id, dto.Email);
                return (false, "Employee with this email already exists");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return false;

            await _repository.DeleteAsync(employee);
            return true;
        }

        public async Task<(bool Success, IEnumerable<string>? Errors)> PatchAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return (false, new[] { "NotFound" });
            // Apply non-null fields from UpdateEmployeeDto
            if (dto.Name is not null) employee.Name = dto.Name;
            if (dto.Department is not null) employee.Department = dto.Department;
            if (dto.Salary.HasValue) employee.Salary = dto.Salary.Value;
            if (dto.Email is not null)
            {
                // check uniqueness
                var existingByEmail = await _repository.GetByEmailAsync(dto.Email);
                if (existingByEmail != null && existingByEmail.EmployeeId != id)
                {
                    return (false, new[] { "Employee with this email already exists" });
                }
                employee.Email = dto.Email;
            }

            // Optional server-side validation: validate updated entity properties
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(employee);
            if (!Validator.TryValidateObject(employee, ctx, validationResults, true))
            {
                return (false, validationResults.Select(v => v.ErrorMessage ?? "Validation error"));
            }

            try
            {
                await _repository.UpdateAsync(employee);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency exception while patching employee id {EmployeeId}", id);
                return (false, new[] { "ConcurrencyError" });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException while patching employee id {EmployeeId} with email {Email}", id, dto.Email);
                return (false, new[] { "Employee with this email already exists" });
            }

            return (true, null);
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
        {
            return await _repository.SearchAsync(keyword);
        }

        public async Task<IEnumerable<Employee>> FilterByDepartmentAsync(string department, int pageNumber = 1, int pageSize = 10)
        {
            return await _repository.FilterByDepartmentAsync(department, pageNumber, pageSize);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }
    }
}

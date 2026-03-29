using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync(int pageNumber, int pageSize);
        Task<Employee?> GetByIdAsync(int id);
        Task<(bool Success, string? Message, Employee? Employee)> CreateAsync(CreateEmployeeDto dto);
        Task<(bool Success, string? Error)> UpdateAsync(int id, CreateEmployeeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<(bool Success, IEnumerable<string>? Errors)> PatchAsync(int id, EmployeeManagementAPI.Models.UpdateEmployeeDto dto);
        Task<IEnumerable<Employee>> SearchAsync(string keyword);
        Task<IEnumerable<Employee>> FilterByDepartmentAsync(string department,int pageNumber = 1,int pageSize = 10);
        Task<int> GetTotalCountAsync();
    }
}

using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(int pageNumber, int pageSize);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Employee>> SearchAsync(string keyword);
        Task<IEnumerable<Employee>> FilterByDepartmentAsync(string department, int pageNumber = 1, int pageSize = 10);
        Task<int> GetTotalCountAsync();
    }
}

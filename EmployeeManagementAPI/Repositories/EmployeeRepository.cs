using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return _context.Employees.FindAsync(id).AsTask();
        }

        public Task<Employee?> GetByEmailAsync(string email)
        {
            return _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(e =>
                    EF.Functions.Like(e.Name, $"{keyword}%") ||
                    EF.Functions.Like(e.Email, $"{keyword}%"))
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email
                })
                .Take(50)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> FilterByDepartmentAsync(string department, int pageNumber, int pageSize)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(e => EF.Functions.Like(e.Department, $"{department}"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees.CountAsync();
        }
    }
}

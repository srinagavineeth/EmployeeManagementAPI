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

        public Task<List<Employee>> GetAllAsync()
        {
            return _context.Employees.ToListAsync();
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
    }
}

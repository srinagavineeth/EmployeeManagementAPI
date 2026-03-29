using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, string message, string? token)> LoginAsync(LoginRequest request);
    }
}
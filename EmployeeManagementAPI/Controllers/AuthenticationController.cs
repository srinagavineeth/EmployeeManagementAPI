using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (success, message, token) = await _authenticationService.LoginAsync(request);

            if (!success)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = message
                });

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = message,
                Data = token
            });
        }
    }
}
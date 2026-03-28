using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class UpdateEmployeeDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Department { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Salary { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }
    }
}

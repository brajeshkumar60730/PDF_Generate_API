using EntityFrameworkSP_Demo.Entities;
using EntityFrameworkSP_Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkSP_Demo.Controllers
{
    [Authorize] // Secures all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [HttpGet("getemployeelist")]
        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            try
            {
                return await employeeService.GetEmployeeListAsync();
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("getemployeebyid")]
        public async Task<IEnumerable<Employee>> GetEmployeeByIdAsync(int Id)
        {
            try
            {
                var response = await employeeService.GetEmployeeByIdAsync(Id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("addemployee")]
        public async Task<IActionResult> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await employeeService.AddEmployeeAsync(employee);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut("updateemployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await employeeService.UpdateEmployeeAsync(employee);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("deleteemployee")]
        public async Task<int> DeleteEmployeeAsync(int Id)
        {
            try
            {
                var response = await employeeService.DeleteEmployeeAsync(Id);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}

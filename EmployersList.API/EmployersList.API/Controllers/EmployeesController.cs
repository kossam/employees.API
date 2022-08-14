using EmployersList.API.Data;
using EmployersList.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployersList.API.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployersDbContext _employersDbContext;

        public EmployeesController(EmployersDbContext employersDbContext)
        {
            _employersDbContext=employersDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees =  await _employersDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _employersDbContext.Employees.AddAsync(employeeRequest);
            await _employersDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
          var employee = await _employersDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                return Ok(employee);
            } 
                
                return NotFound();
           

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _employersDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;
            employee.Email = updateEmployeeRequest.Email;

            await _employersDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
          var employee = await _employersDbContext.Employees.FindAsync(id);
            if(employee == null) return NotFound();

            _employersDbContext.Employees.Remove(employee);
            await _employersDbContext.SaveChangesAsync();
            return Ok(employee);    
        }
    }
}

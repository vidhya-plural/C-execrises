using EmployeesInformation.Data;
using EmployeesInformation.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesInformation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = EmployeesDataStore.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = EmployeesDataStore.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.Name) || employee.Salary < 0)
            {
                return BadRequest("Invalid employee data.");
            }

            var success = EmployeesDataStore.InsertEmployee(employee.Name, employee.JobTitle, employee.Salary);
            if (!success)
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var success = EmployeesDataStore.DeleteEmployee(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

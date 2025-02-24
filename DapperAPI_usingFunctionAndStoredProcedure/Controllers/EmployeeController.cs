using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI_usingFunctionAndStoredProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAllEmployee")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            List<Employee> deptlist = (List<Employee>)await _repository.GetAllAsync();
            return Ok(deptlist);
        }

        [HttpGet("id", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                Employee obj = await _repository.GetByIdAsync(id);
                return Ok(obj);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null || employee.EmployeeId < 0)
            {
                return BadRequest();
            }
            try
            {
                await _repository.AddItemAsync(employee);
                return Ok(employee);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee.EmployeeId == 0)
            {
                return BadRequest();
            }
            Employee obj = await _repository.GetByIdAsync(employee.EmployeeId);
            if (obj == null)
            {
                return NotFound();
            }
            await _repository.UpdateAsync(employee);
            return Ok(employee);
        }

    }
}

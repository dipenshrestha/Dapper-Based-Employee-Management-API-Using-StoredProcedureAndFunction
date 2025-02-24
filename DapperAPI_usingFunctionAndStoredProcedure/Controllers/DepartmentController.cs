using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI_usingFunctionAndStoredProcedure.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet(Name = "GetAllDepartment")]
        public async Task<ActionResult<List<Department>>> GetAllDepartment()
        {
            List<Department> deptlist = (List<Department>)await _repository.GetAllAsync();
            return Ok(deptlist);
        }

        //Controller For Get Department by Id
        [HttpGet("id", Name = "GetDepartmentById")]
        //helps in documenting the API for clients and tools like Swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Department>> GetDepartmentAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                Department obj = await _repository.GetDepartmentById(id);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            //if (obj == null)
            //{
            //    return NotFound(new { message = $"Department with ID {id} not found." });
            //}
            //return Ok(obj);
        }

        [HttpPost(Name = "CreateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //the [FromBody] annotation is use to Ensures JSON data is correctly deserialized into the method parameter.
        // or to bind request body (used in Po)
        //for clarity to multiple complex parameters
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.AddItemAsync(obj);
                    return Ok("Department added successfully.");
                }
                catch (Exception ex)
                {
                    return Conflict(ex.Message); // 409 Conflict if Department exists
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                Department obj = await _repository.GetDepartmentById(id);
                await _repository.DeleteAsync(obj);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Department>> Update([FromBody] Department obj)
        {
            if (obj.DepartmentId == 0)
            {
                return BadRequest();
            }
            Department item = await _repository.GetDepartmentById(obj.DepartmentId);
            if (item == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(obj);
            return Ok(obj);
        }
    }
}

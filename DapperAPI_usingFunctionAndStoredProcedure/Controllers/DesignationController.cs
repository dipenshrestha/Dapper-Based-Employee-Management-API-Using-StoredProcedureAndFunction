using DapperAPI_usingFunctionAndStoredProcedure.IRepository;
using DapperAPI_usingFunctionAndStoredProcedure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI_usingFunctionAndStoredProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _repository;
        public DesignationController(IDesignationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAllDesignation")]
        public async Task<ActionResult<List<Designation>>> GetAllDesignation()
        {
            List<Designation> desigList = (List<Designation>)await _repository.GetAllAsync();
            return Ok(desigList);
        }


        [HttpGet("id", Name = "GetDesignationById")]
        //helps in documenting the API for clients and tools like Swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Designation>> GetDesignationById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                Designation obj = await _repository.GetDesignationById(id);
                return Ok(obj);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            //if (obj == null)
            //{
            //    return NotFound(new { message = $"Designation with ID {id} not found." });
            //} 
        }

        [HttpPost(Name = "CreateDesignation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Designation>> CreateDesignation([FromBody] Designation obj)
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
                    return Ok("Designation created Successfully");
                }
                catch (InvalidOperationException ex)
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
            Designation obj = await _repository.GetDesignationById(id);
            if (obj == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(obj);
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Designation>> Update([FromBody] Designation obj)
        {
            if (obj.DesignationId == 0)
            {
                return BadRequest();
            }
            Designation item = await _repository.GetDesignationById(obj.DesignationId);
            if (item == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(obj);
            return Ok(obj);
        }
    }
}

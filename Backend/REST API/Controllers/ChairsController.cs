using ChairsLib;
using ChairsLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChairsController : ControllerBase
    {
        private readonly IChairsRepository _repository;

        public ChairsController(IChairsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ChairsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Chair>> Get()
        {
            IEnumerable<Chair> chairs = _repository.GetAll();
            return Ok(chairs);
        }

        // GET api/<ChairsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Get(int id)
        {
            Chair? foundChair = _repository.GetById(id);
            if (foundChair == null)
            {
                return NotFound();
            }

            return Ok(foundChair);
        }

        // POST api/<ChairsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Chair> Post([FromBody] Chair newChair)
        {
            Chair createdChair = _repository.Add(newChair);
            return CreatedAtAction(nameof(Get), new { id = createdChair.Id }, createdChair);
        }

        // PUT api/<ChairsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Put(int id, [FromBody] Chair newValues)
        {
            Chair? updatedChair = _repository.Update(id, newValues);
            if (updatedChair == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(updatedChair);
            }
        }

        // DELETE api/<ChairsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Delete(int id)
        {
            Chair? deletedChair = _repository.Delete(id);
            if (deletedChair == null)
            {
                return NotFound();
            }

            return Ok(deletedChair);
        }
    }
}

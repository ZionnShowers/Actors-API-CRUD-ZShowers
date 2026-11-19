using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorServices _services;

        public ActorController(IActorServices services)
        {
            _services = services;
        }


        [HttpGet]
        public async Task<ActionResult<List<ActorResponseDto>>> GetAll()
        {
            return Ok(await _services.GetAllAsync());
        }


        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ActorResponseDto>> GetById(int id)
        {
            ActorResponseDto? actor = await _services.GetByIdAsync(id);

            if(actor == null)
            {
                return NotFound($"No actor with that id exists.");
            }
            return Ok(actor);
        }


        [HttpPost("AddActor")]
        public async Task<ActionResult<ActorResponseDto>> AddActor([FromBody] CreateActorDto dto)
        {
            ActorResponseDto actor = await _services.CreateAsync(dto);
            if(actor is null)
            {
                return Conflict($"There is already an actor with the same name.");
            }

            return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor);
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ActorResponseDto>> Update(int id, [FromBody] UpdateActorDto dto)
        {
            ActorResponseDto? actor = await _services.UpdateAsync(id, dto);

            if(actor is null)
            {
                return NotFound($"No actor with that id exists.");
            }

            return Ok(actor);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _services.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"No actor with that id exists.");
            }

            return NoContent();
        }
    }
}
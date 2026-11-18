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
        public ActionResult<List<ActorResponseDto>> GetAll()
        {
            return Ok(_services.GetAll());
        }


        [HttpGet("GetById/{id}")]
        public ActionResult<ActorResponseDto> GetById(int id)
        {
            ActorResponseDto actor = _services.GetById(id);

            if(actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }


        [HttpPost("AddActor")]
        public ActionResult<ActorResponseDto> AddActor([FromBody] CreateActorDto dto)
        {
            ActorResponseDto actor = _services.Create(dto);

            return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor);
        }


        [HttpPut("Update/{id}")]
        public ActionResult<ActorResponseDto> Update(int id, [FromBody] UpdateActorDto dto)
        {
            ActorResponseDto? actor = _services.Update(id, dto);

            if(actor is null)
            {
                return NotFound();
            }

            return Ok(actor);
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _services.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
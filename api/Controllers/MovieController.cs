using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

//Our Controller Layer: Takes in HTTP Requests and Sends back HTTP Response.
//It only talks to our Services, Never to our DB.

namespace api.Controllers
{
    [ApiController] //This line turns on our API Features Like Automatic 400 status code
    [Route("api/[controller]")] //Makes every url endpoint start with api/movie
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _services;

        public MovieController(IMovieServices services)
        {
            _services = services;
        }

        //async Task<..>: the action awaits the services, so it returns a Task
        //ASP.NET Core awaits the Task for us, then sends the response.

        [HttpGet]
        public async Task<ActionResult<List<MovieResponseDto>>> GetAll()
        {
            //await frees this thread while the database works,
            //so the server can handle other requests in the meantime.
            return Ok(await _services.GetAllAsync());
        }

        //Get api/movie/getbyid/5
        //return 200 with the movie, or 400 if no movie is in the db
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MovieResponseDto>> GetById(int id)
        {
            MovieResponseDto movie = await _services.GetByIdAsync(id);

            if(movie == null)
            {
                return NotFound(); // sends a 404 response
            }
            return Ok(movie);
        }

        //Post api/movie/addmovie
        //JSON body becomes our CreateMovieDto
        //return 201 Created with the new movie, plus our location headers
        [HttpPost("AddMovie")]
        public async Task<ActionResult<MovieResponseDto>> AddMovie([FromBody] CreateMovieDto dto)
        {
            MovieResponseDto movie = await _services.CreateAsync(dto);

            //created at action sends 201, It uses GetById id action and the new Id
            //To build the location headers
            return CreatedAtAction(nameof(GetById), new {id = movie.Id}, movie);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<MovieResponseDto>> Update(int id, [FromBody] UpdateMovieDto dto)
        {
            MovieResponseDto? movie = await _services.UpdateAsync(id, dto);

            if(movie is null)
            {
                return NotFound(); //Movie is not found in our Database send a 404
            }

            return Ok(movie);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _services.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            //This worked and theres nothing to send back 204 status code
            return NoContent();
        }
    }
}
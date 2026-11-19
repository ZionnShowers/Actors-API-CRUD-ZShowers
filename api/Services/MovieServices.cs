//Our Services Layer is the middle man between our Controller and Repository
//It maps our Dtos to entitied (and back) And this iis where our rules live
using api.Dtos;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class MovieServices : IMovieServices
    {

        private readonly IMovieRepository _repository;

        //DI: in our Program.cs says IMovieRepository mean MovieRepository
        public MovieServices(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieResponseDto> CreateAsync(CreateMovieDto dto)
        {
            Movie movie = new Movie();
            movie.Title = dto.Title;
            movie.Director = dto.Director;
            movie.Rating = dto.Rating;
            movie.ReleaseYear = dto.ReleaseYear;

            Movie saved = await _repository.AddAsync(movie);
            //Sending back the same movie but now with an Id
            return ToDto(saved);
        }

        //This method awaits the repository layer, so it HAS to be async and return a Task Too.
        //Once the bottom layer is async, every layer that calls it becomes async as well.
        public async Task<List<MovieResponseDto>> GetAllAsync()
        {
            // await waits for the database without blocking the thread.
            List<Movie> movies = await _repository.GetAllAsync();

            //Turning every Movie entity inta a MovieResponseDto.
            return movies.Select(ToDto).ToList();
        }

        public async Task<MovieResponseDto?> GetByIdAsync(int id)
        {
            Movie? movie = await _repository.GetByIdAsync(id);
            //Iff there is no movie pass null up to the controller
            if(movie is null)
            {
                return null;
            }
            return ToDto(movie);
        }

        //We will Create a Helper Function that maps our Dto
        //private because it diesn't need to be accessed outside of our class
        //static because it doesn't have access to instanc data
        private static MovieResponseDto ToDto(Movie movie)
        {
            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating
            };
        }

        public async Task<MovieResponseDto?> UpdateAsync(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? movie = await _repository.GetByIdAsync(id);

            //if your id isn't in the database we return null
            if(movie is null)
            {
                return null;
            }

            movie.Title = dto.Title;
            movie.Director = dto.Director;
            movie.Rating = dto.Rating;
            movie.ReleaseYear = dto.ReleaseYear;

            //Record when this movie is last changed
            movie.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(movie);

            return ToDto(movie);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Movie? movie = await _repository.GetByIdAsync(id);

            if(movie is null)
            {
                //False tells our controller no movie was found send 404;
                return false;
            }

            await _repository.DeleteAsync(movie);
            return true;
        }
    }
}
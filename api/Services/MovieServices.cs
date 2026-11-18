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

        public MovieResponseDto Create(CreateMovieDto dto)
        {
            Movie movie = new Movie();
            movie.Title = dto.Title;
            movie.Director = dto.Director;
            movie.Rating = dto.Rating;
            movie.ReleaseYear = dto.ReleaseYear;

            Movie saved = _repository.Add(movie);
            //Sending back the same movie but now with an Id
            return ToDto(saved);
        }

        public List<MovieResponseDto> GetAll()
        {
            List<Movie> movies = _repository.GetAll();

            //Turning every element in this list into a dto
            return movies.Select(ToDto).ToList();
        }

        public MovieResponseDto? GetById(int id)
        {
            Movie? movie = _repository.GetById(id);
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

        public MovieResponseDto Update(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? movie = _repository.GetById(id);

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

            _repository.Update(movie);

            return ToDto(movie);
        }

        public bool Delete(int id)
        {
            Movie? movie = _repository.GetById(id);

            if(movie is null)
            {
                //False tells our controller no movie was found send 404;
                return false;
            }

            _repository.Delete(movie);
            return true;
        }
    }
}
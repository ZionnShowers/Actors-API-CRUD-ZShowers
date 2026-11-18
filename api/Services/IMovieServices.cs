//This services interface is a list of jobs that our Services must implement
//Our Services speak in DTO, So the controller never touckes the Entities
using api.Dtos;

namespace api.Services
{
    public interface IMovieServices
    {
        List<MovieResponseDto> GetAll();
        MovieResponseDto? GetById(int id);
        MovieResponseDto Create(CreateMovieDto dto);

        //This will return null if the movie doesn't exist
        MovieResponseDto Update(int id, UpdateMovieDto dto);

        //Returns true if the movie is deleted
        bool Delete(int id);
    }
}
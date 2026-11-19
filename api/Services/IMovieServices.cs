//This services interface is a list of jobs that our Services must implement
//Our Services speak in DTO, So the controller never touckes the Entities
using api.Dtos;

namespace api.Services
{
    public interface IMovieServices
    {
        //Every method returns a Task, so the caller can await it.
        //Task<bool> for example means "a bool that will be ready later"
        Task<List<MovieResponseDto>> GetAllAsync();
        Task<MovieResponseDto?> GetByIdAsync(int id);
        Task<MovieResponseDto> CreateAsync(CreateMovieDto dto);

        //This will return null if the movie doesn't exist
        Task<MovieResponseDto?> UpdateAsync(int id, UpdateMovieDto dto);

        //Returns true if the movie is deleted
        Task<bool> DeleteAsync(int id);
    }
}
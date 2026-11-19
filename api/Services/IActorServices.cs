//This services interface is a list of jobs that our Services must implement
//Our Services speak in DTO, So the controller never touckes the Entities
using api.Dtos;

namespace api.Services
{
    public interface IActorServices
    {
        Task<List<ActorResponseDto>> GetAllAsync();
        Task<ActorResponseDto?> GetByIdAsync(int id);
        Task<ActorResponseDto> CreateAsync(CreateActorDto dto);

        Task<ActorResponseDto?> UpdateAsync(int id, UpdateActorDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
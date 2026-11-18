//This services interface is a list of jobs that our Services must implement
//Our Services speak in DTO, So the controller never touckes the Entities
using api.Dtos;

namespace api.Services
{
    public interface IActorServices
    {
        List<ActorResponseDto> GetAll();
        ActorResponseDto? GetById(int id);
        ActorResponseDto Create(CreateActorDto dto);

        ActorResponseDto Update(int id, UpdateActorDto dto);

        bool Delete(int id);
    }
}
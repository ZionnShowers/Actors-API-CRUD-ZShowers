//DTOS are JSON shapes that the client sees. The Movie entity stays inside the API

//No CreatedAt or UpdatedAt: the client only gets the fields we choose to share.
namespace api.Dtos
{
    public class ActorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set;} = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int BirthYear { get; set; }
    }
}
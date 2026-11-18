//What the client sends to change a movie. The Id comes in from the URL
namespace api.Dtos
{
    public class UpdateActorDto
    {
        public string Name { get; set;} = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int BirthYear { get; set; }
    }
}
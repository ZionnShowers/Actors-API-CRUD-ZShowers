//This is what the client sends to create a movie, No Id, the database makes that.
namespace api.Dtos
{
    public class CreateActorDto
    {
        public string Name { get; set;} = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int BirthYear { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
namespace api.Dtos
{
    public class CreateActorDto
    {
        [Required(ErrorMessage = "Must input a unique Name")]
        public string Name { get; set;} = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int BirthYear { get; set; }
    }
}
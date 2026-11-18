
using api.Models;

namespace api.Repositories
{
    public interface IMovieRepository
    {
        //The repository interface: the list of database jobs for movies.
        //Repositories work with entities (Movie), Never DTOs
        List<Movie> GetAll();
        Movie? GetById(int id);
        //Returns one movie, or null if no movie has that id (That's what ? means).
        Movie Add(Movie movie);

        void Update(Movie movie);

        void Delete(Movie movie);
    }
}
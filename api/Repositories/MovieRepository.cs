using api.Data;
using api.Models;

namespace api.Repositories
{
    //The repository layer: the ONLY layer that talks to the database.
    // It uses AppDbContext (Ef Core) to read and save Movie Entities
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;

        //DI: we never write a "new AppDbContext()" ourselves
        //ASP.NET Core sees this contructor asks for one and passes it in for us.
        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Movie> GetAll()
        {
            //we are going into our DB -> Movies Tables and returning all movies
            return _db.Movies.ToList();
        }

        public Movie? GetById(int id)
        {
            //Find loops up a row by its primary key. It returns null if there is no match
            return _db.Movies.Find(id);
        }

        public Movie Add(Movie movie)
        {
            //Add only marks the movie as new. Nothing is in the database yet.
            _db.Movies.Add(movie);

            //SaveChanges() sends INSERT to the database. After this, Movie row is filled in.
            _db.SaveChanges();
            return movie;
        }

        public void Update(Movie movie)
        {
            //We mark the movie is changed, and then save those changes to the DB
            _db.Movies.Update(movie);
            _db.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            //We mark the movie for removal, then SaveChanges send the delete
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }
    }
}
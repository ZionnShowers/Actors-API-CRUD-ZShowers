using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        //async lets this method use await inside it.
        public async Task<List<Movie>> GetAllAsync()
        {
            //we are going into our DB -> Movies Tables and returning all movies
            //await while DB is working. This thread is freed to handle other requests
            return await _db.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            //FindAsync loops up a row by its primary key. It returns null if there is no match
            return await _db.Movies.FindAsync(id);
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            //Add only marks the movie as new. Nothing is in the database yet.
            //That's why Add has no await, it doesn't talk to the database
            _db.Movies.Add(movie);

            //SaveChanges() sends INSERT to the database. After this, Movie row is filled in.
            await _db.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            //We mark the movie is changed, and then save those changes to the DB
            _db.Movies.Update(movie);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            //We mark the movie for removal, then SaveChanges send the delete
            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
        }
    }
}
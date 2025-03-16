using Movies.Application.Models;

namespace Movies.Application.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly List<Movie> _movies = [];

    public Task<bool> CreateAsync(Movie movie)
    {
        _movies.Add(movie);
        
        return Task.FromResult(true);
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        var movie = _movies.SingleOrDefault(m => m.Id == id);
        
        return Task.FromResult(movie);
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        var items = _movies.AsEnumerable();
        
        return Task.FromResult(items);
    }

    public Task<bool> UpdateAsync(Movie movie)
    {
        var movieIndex = _movies.IndexOf(movie);
        if (movieIndex == -1)
        {
            return Task.FromResult(false);
        }
        
        _movies[movieIndex] = movie;
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var removedCount = _movies.RemoveAll(m => m.Id == id);
        var isRemoved = removedCount > 0;
        
        return Task.FromResult(isRemoved);
    }
}
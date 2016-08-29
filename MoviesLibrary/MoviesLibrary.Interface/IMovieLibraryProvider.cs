using System.Collections.Generic;

namespace MoviesLibrary.Interface
{
    /// <summary>
    /// Defines the operations that can be performed on a <see cref="Movie"/>
    /// </summary>
    public interface IMovieLibraryProvider
    {
        /// <summary>
        /// Creates a movie in movie library
        /// </summary>
        /// <param name="movie">Movie to be created</param>
        /// <returns>Movie Id</returns>
        int Create(Movie movie);

        /// <summary>
        /// Returns the collection of movies 
        /// </summary>
        /// <returns>Collection of <see cref="Movie"/></returns>
        List<Movie> GetAllData();

        /// <summary>
        /// Returns a movie for id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Movie of type <see cref="Movie"/></returns>
        Movie GetDataById(int id);

        /// <summary>
        /// Updates the movie in movie library
        /// </summary>
        /// <param name="movie"></param>
        void Update(Movie movie);

        /// <summary>
        /// Returns a collection of movies matching search criteria
        /// </summary>
        /// <param name="searchText">text to search in movies library</param>
        /// <returns>Collection of <see cref="Movie"/></returns>
        List<Movie> SearchMovies(string searchText);
    }
}

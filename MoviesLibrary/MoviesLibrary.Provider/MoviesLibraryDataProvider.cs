using System;
using System.Collections.Generic;
using System.Linq;
using MoviesLibrary.Interface;
using log4net;
using MoviesLibrary.Provider.Repository;

namespace MoviesLibrary.Provider
{
    /// <summary>
    /// The actual provider implementation to retrieve/create/update/search movies 
    /// </summary>
    public class MoviesLibraryDataProvider : IMovieLibraryProvider
    {
        /// <summary>
        /// Logger to log debug/error logs
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly  IRepository<Movie> _repository;
        public MoviesLibraryDataProvider()
        {
            _repository = new MoviesRepository();
            
        }

        /// <summary>
        /// Creates the Movie in movie library 
        /// </summary>
        /// <param name="movie"> the movie to be created</param>
        /// <returns></returns>
        public int Create(Movie movie)
        {
            try
            {
                return _repository.Create(movie);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[Provider - Create] Unable to create new movie to library due to following error: {0}", ex.InnerException);
                return -1;
            }
        }

        /// <summary>
        /// Get All movies from movie library 
        /// </summary>
        /// <returns>List of Movies from movie library</returns>
        public List<Movie> GetAllData()
        {
            try
            {
                return _repository.GetAllData().ToList();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[Provider - GetAllData] Unable to retrieve all movies from library due to following error: {0}", ex.InnerException);
                return null;
            }
        }

        /// <summary>
        /// Get Movie details by MovieId
        /// </summary>
        /// <param name="movieId">Unique Id</param>
        /// <returns>Movie for passed movieId</returns>
        public Movie GetDataById(int movieId)
        {
            try
            {
                return _repository.GetDataById(movieId);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[Provider - GetDataById] Unable to retrieve movie by Id from library due to following error: {0}", ex.InnerException);
                return null;
            }
            
        }

        /// <summary>
        /// Update an existing movie details
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        public void Update(Movie movie)
        {
            try
            {
                _repository.Update(movie);
            }
            catch (Exception ex)
            {
               Log.ErrorFormat("[Provider - Update] Unable to update movie due to following error: {0}", ex.InnerException);
            }
        }

        /// <summary>
        /// Search movies from the store for given criteria
        /// </summary>
        /// <param name="searchText"> the search criteria</param>
        /// <returns>List of movies matching search criteria</returns>
        public List<Movie> SearchMovies(string searchText)
        {
            try
            {
                return _repository.SearchMovies(searchText).ToList();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[Provider - SearchMovies] Unable to retrieve movies for search criteria [{0}] from library due to following error: {1}",searchText, ex.InnerException);
                return null;
            }
        }
    }
}

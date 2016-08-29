using System.ServiceModel;
using MoviesLibrary.Interface;
using MoviesLibrary.Service.ServiceContracts;

namespace MoviesLibrary.Service
{
    /// <summary>
    /// This is an interface to define the operations that will be exposed in the MovieLibrary Service
    /// </summary>
    [ServiceContract]
    public interface IMoviesLibraryService
    {
        /// <summary>
        /// Gets All movies from movie library 
        /// </summary>
        /// <returns>A <see cref="GetAllMoviesResponse"/> that contains the collection of <see cref="Movie"/></returns>
        [OperationContract]
        GetAllMoviesResponse GetAllMovies();

        /// <summary>
        /// Get Movie details by MovieId
        /// </summary>
        /// <param name="request">A <see cref="GetMovieByIdRequest"/> that contains a <see cref="int"/> movie id</param>
        /// <returns>A <see cref="GetMovieByIdResponse"/> that contains the <see cref="Movie"/></returns>
        [OperationContract]
        GetMovieByIdResponse GetMovieById(GetMovieByIdRequest request);

        /// <summary>
        /// Update an existing movie details
        /// </summary>
        /// <param name="request">A <see cref="UpdateMovieRequest"/> that contains a <see cref="Movie"/></param>
        /// <returns>A <see cref="UpdateMovieResponse"/> that contains the <see cref="int"/> movie id</returns>
        [OperationContract]
        UpdateMovieResponse UpdateMovie(UpdateMovieRequest request);

        /// <summary>
        /// Creates the Movie in movie library 
        /// </summary>
        /// <param name="request">A <see cref="CreateMovieRequest"/> that contains a <see cref="Movie"/></param>
        /// <returns>A <see cref="CreateMovieResponse"/> that contains the <see cref="int"/> movie id</returns>
        [OperationContract]
        CreateMovieResponse CreateMovie(CreateMovieRequest request);

        /// <summary>
        /// Search movies from the store for given criteria
        /// </summary>
        /// <param name="request">A <see cref="SearchMoviesRequest"/> that contains a <see cref="string"/> search criteria</param>
        /// <returns>A <see cref="GetAllMoviesResponse"/> that contains the <see cref="Movie"/></returns>
        [OperationContract]
        GetAllMoviesResponse SearchMovies(SearchMoviesRequest request);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MoviesLibrary.Interface;
using MoviesLibrary.Service.ServiceContracts;
using log4net;
using MoviesLibraryService.MessageContracts;
using System.Xml;
using System.Xml.Linq;

namespace MoviesLibrary.Service
{
    /// <summary>
    /// The actual Service implementation to retrieve/create/update/search movies 
    /// </summary>
    public class MoviesLibraryService : IMoviesLibraryService
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMovieLibraryProvider _movieLiraryProvider;

        public MoviesLibraryService(IMovieLibraryProvider movieLibraryProvider)
        {
            _movieLiraryProvider = movieLibraryProvider;
        }
        
        /// <summary>
        /// Implementation of <see cref="IMoviesLibraryService.GetAllMovies"/>
        /// </summary>
        /// <returns>A <see cref="GetAllMoviesResponse"/> that contains the collection of <see cref="Movie"/></returns>
        public GetAllMoviesResponse GetAllMovies()
        {
            var response = new GetAllMoviesResponse {Movies = new List<DataContracts.Movie>()};
            Log.Debug("About to retrieve All Movies from MoviesLibrary");
            try
            {
                var moviesList = _movieLiraryProvider.GetAllData();
                foreach (var dcMovie in moviesList.Select(movie => new DataContracts.Movie
                {
                    Cast = movie.Cast,
                    Classification = movie.Classification,
                    Genre = movie.Genre,
                    MovieId = movie.MovieId,
                    Rating = movie.Rating,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title
                }))
                {
                    response.Movies.Add(dcMovie);
                }
                Log.Debug("Successfully retrieved All Movies from MoviesLibrary");
            }
            catch (Exception ex)
            {
                throw GetFaultException(null, ex);
            }
            return response;
        }

        /// <summary>
        ///  Implementation of <see cref="IMoviesLibraryService.GetMovieById"/>
        /// </summary>
        /// <param name="request">A <see cref="GetMovieByIdRequest"/> that contains a <see cref="int"/> movie id</param>
        /// <returns>A <see cref="GetMovieByIdResponse"/> that contains the <see cref="Movie"/></returns>
        public GetMovieByIdResponse GetMovieById(GetMovieByIdRequest request)
        {
            var response = new GetMovieByIdResponse();
            Log.DebugFormat( "About to get Movie from MoviesLibrary with Id [{0}]", request.MovieId);
            try
            {
                var movie = _movieLiraryProvider.GetDataById(request.MovieId);
                var dcMovie = new DataContracts.Movie
                {
                    Cast = movie.Cast,
                    Classification = movie.Classification,
                    Genre = movie.Genre,
                    MovieId = movie.MovieId,
                    Rating = movie.Rating,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title
                };
                response.Movie = dcMovie;
                Log.DebugFormat("Successfully retrieved Movie from MoviesLibrary with Id [{0}]", request.MovieId);
            }
            catch (Exception ex)
            {
                throw GetFaultException(request, ex);
            }
            return response;
        }

        /// <summary>
        /// Implementation of <see cref="IMoviesLibraryService.UpdateMovie"/>
        /// </summary>
        /// <param name="request">A <see cref="UpdateMovieRequest"/> that contains a <see cref="Movie"/></param>
        /// <returns>A <see cref="UpdateMovieResponse"/> that contains the <see cref="int"/> movie id</returns>
        public UpdateMovieResponse UpdateMovie(UpdateMovieRequest request)
        {
            var movie = new Movie();
            Log.DebugFormat("About to update Movie from MoviesLibrary with Id [{0}]", request.Movie.MovieId);
            try
            {
                movie.Cast = request.Movie.Cast;
                movie.Classification = request.Movie.Classification;
                movie.Genre = request.Movie.Genre;
                movie.MovieId = request.Movie.MovieId;
                movie.Rating = request.Movie.Rating;
                movie.ReleaseDate = request.Movie.ReleaseDate;
                movie.Title = request.Movie.Title;

                _movieLiraryProvider.Update(movie);
                Log.DebugFormat("Successfully updated Movie from MoviesLibrary with Id [{0}]", request.Movie.MovieId);
                return new UpdateMovieResponse
                {
                    MovieId = request.Movie.MovieId
                };
            }
            catch (Exception ex)
            {
                throw GetFaultException(request, ex);
            }
        }

        /// <summary>
        ///  Implementation of <see cref="IMoviesLibraryService.CreateMovie"/>
        /// </summary>
        /// <param name="request">A <see cref="CreateMovieRequest"/> that contains a <see cref="Movie"/></param>
        /// <returns>A <see cref="CreateMovieResponse"/> that contains the <see cref="int"/> movie id</returns>
        public CreateMovieResponse CreateMovie(CreateMovieRequest request)
        {
            var movie = new Movie();
            Log.Debug("About to create a new Movie in MoviesLibrary");
            try
            {
                movie.Cast = request.Movie.Cast;
                movie.Classification = request.Movie.Classification;
                movie.Genre = request.Movie.Genre;
                movie.Rating = request.Movie.Rating;
                movie.ReleaseDate = request.Movie.ReleaseDate;
                movie.Title = request.Movie.Title;

                _movieLiraryProvider.Create(movie);
                Log.Debug("Successfully created a new Movie in MoviesLibrary");
                return new CreateMovieResponse
                {
                    MovieId = request.Movie.MovieId
                };
            }
            catch (Exception ex)
            {
                throw GetFaultException(request, ex);
            }
            
        }

        /// <summary>
        /// Implementation of <see cref="IMoviesLibraryService.SearchMovies"/>
        /// </summary>
        /// <param name="request">A <see cref="SearchMoviesRequest"/> that contains a <see cref="string"/> search criteria</param>
        /// <returns>A <see cref="GetAllMoviesResponse"/> that contains the <see cref="Movie"/></returns>
        public GetAllMoviesResponse SearchMovies(SearchMoviesRequest request)
        {
            var response = new GetAllMoviesResponse {Movies = new List<DataContracts.Movie>()};
            Log.Debug(string.Format("About to Search Movies from MoviesLibrary with criteria {0}", request.SearchText));
            try
            {
                var moviesList = _movieLiraryProvider.SearchMovies(request.SearchText);
                foreach (var movie in moviesList)
                {
                    var dcMovie = new DataContracts.Movie
                    {
                        Cast = movie.Cast,
                        Classification = movie.Classification,
                        Genre = movie.Genre,
                        MovieId = movie.MovieId,
                        Rating = movie.Rating,
                        ReleaseDate = movie.ReleaseDate,
                        Title = movie.Title
                    };
                    response.Movies.Add(dcMovie);
                }
                Log.Debug(string.Format("Successfully searched Movies from MoviesLibrary with criteria {0}", request.SearchText));
            }
            catch (Exception ex)
            {
                throw GetFaultException(request, ex);
            }
            return response;
        }

        /// <summary>
        /// Exception Handling routine.
        /// This standardizes logging, Exception to faultexception mapping, and returns the mapped FaultException
        /// </summary>
        /// <param name="request">Operation Request</param>
        /// <param name="ex">The exception that was thrown</param>
        /// <returns></returns>
        private static FaultException GetFaultException(RequestMessageBase request, Exception ex)
        {
            var message = string.Format("An exception occurred while executing service operation {0}. \r\n\n Request Message : {1}",
                request.GetType().Name.Replace("Request", string.Empty),
                GetSerializedDataContract(request));
            
            var faultException = (FaultException)ex;
            if (faultException is FaultException<SystemException>)
                Log.Fatal(message, ex.GetBaseException());
            else
                Log.Error(message, ex.GetBaseException());
            return faultException;
        }

        /// <summary>
        /// Serializes the message/data contract to XML
        /// </summary>
        /// <param name="dataContract">The contract to be serialized</param>
        /// <returns>The <see cref="string"/> XML representation of the message/data contract</returns>
        private static string GetSerializedDataContract(object dataContract)
        {
            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb);
            var serializer = new DataContractSerializer(dataContract.GetType());
            serializer.WriteObject(writer, dataContract);
            writer.Close();
            var doc = XDocument.Parse(sb.ToString());
            return doc.ToString(SaveOptions.None);

        }
    }
}

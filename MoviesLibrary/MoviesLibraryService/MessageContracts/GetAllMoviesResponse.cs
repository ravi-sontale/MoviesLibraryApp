using System.Collections.Generic;
using System.ServiceModel;
using MoviesLibrary.Service.DataContracts;

namespace MoviesLibrary.Service
{
    /// <summary>
    /// This is a response message for the <see cref="IMoviesLibraryService.GetAllMovies"/> operation that
    /// contains all data returned by the operation
    /// </summary>
    [MessageContract]
    public class GetAllMoviesResponse
    {
        /// <summary>
        /// Returns collection of <see cref="Movie"/> from movie library
        /// </summary>
        [MessageBodyMember]
        public IList<Movie> Movies { get; set; }
    }
}

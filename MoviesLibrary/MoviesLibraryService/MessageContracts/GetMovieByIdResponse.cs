using System.ServiceModel;
using MoviesLibrary.Service.DataContracts;


namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// This is a response message for the <see cref="IMoviesLibraryService.GetMovieById"/> operation that
    /// contains all data returned by the operation
    /// </summary>
    [MessageContract]
    public class GetMovieByIdResponse
    {
        /// <summary>
        /// Returns a <see cref="Movie"/> 
        /// </summary>
        [MessageBodyMember]
        public Movie Movie { get; set; }
    }
}

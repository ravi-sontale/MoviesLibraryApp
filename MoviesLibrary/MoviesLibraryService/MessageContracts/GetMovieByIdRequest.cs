using System.ServiceModel;
using MoviesLibraryService.MessageContracts;

namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// A class represents a request message for the <see cref="IMoviesLibraryService.GetMovieById"/> operation
    /// that contains all needed data to perform operaton
    /// </summary>
    [MessageContract]
    public class GetMovieByIdRequest :RequestMessageBase
    {
        /// <summary>
        /// A <see cref="int"/> movie id that uniquely identifies the movie
        /// </summary>
        [MessageBodyMember]
        public int MovieId { get; set; }
    }
}

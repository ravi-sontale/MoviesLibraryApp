using System.ServiceModel;
using MoviesLibrary.Service.DataContracts;
using MoviesLibraryService.MessageContracts;

namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// A class represents a request message for the <see cref="IMoviesLibraryService.UpdateMovie"/> operation
    /// that contains all needed data to perform operaton
    /// </summary>
    [MessageContract]
    public class UpdateMovieRequest : RequestMessageBase
    {
        /// <summary>
        /// A <see cref="Movie"/> to be updated
        /// </summary>
        [MessageBodyMember]
        public Movie Movie { get; set; }
    }
}

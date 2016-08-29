using System.ServiceModel;
using MoviesLibraryService.MessageContracts;

namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// A class represents a request message for the <see cref="IMoviesLibraryService.SearchMovies"/> operation
    /// that contains all needed data to perform operaton
    /// </summary>
    [MessageContract]
    public class SearchMoviesRequest : RequestMessageBase
    {
        /// <summary>
        /// A <see cref="string"/> to search in movie library
        /// </summary>
        [MessageBodyMember]
        public string SearchText { get; set; }
    }
}

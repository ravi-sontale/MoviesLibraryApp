using System.ServiceModel;

namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// This is a response message for the <see cref="IMoviesLibraryService.CreateMovie"/> operation that
    /// contains all data returned by the operation
    /// </summary>
    [MessageContract]
    public class CreateMovieResponse 
    {
        /// <summary>
        /// A <see cref="int"/> movie id of newly created movie
        /// </summary>
        [MessageBodyMember]
        public int MovieId { get; set; }
    }
}

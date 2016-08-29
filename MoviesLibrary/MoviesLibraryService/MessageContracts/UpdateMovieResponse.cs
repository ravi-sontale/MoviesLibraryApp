using System.ServiceModel;

namespace MoviesLibrary.Service.ServiceContracts
{
    /// <summary>
    /// This is a response message for the <see cref="IMoviesLibraryService.UpdateMovie"/> operation that
    /// contains all data returned by the operation
    /// </summary>
    [MessageContract]
    public class UpdateMovieResponse 
    {
        /// <summary>
        /// Returns <see cref="int"/> MovieId
        /// </summary>
        [MessageBodyMember]
        public int MovieId { get; set; }
    }
}

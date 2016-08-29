using System.Runtime.Serialization;

namespace MoviesLibrary.Service.DataContracts
{
    /// <summary>
    /// A Class represents a Movie
    /// </summary>
    [DataContract]
    public class Movie
    {
        /// <summary>
        /// A <see>
        ///     <cref>string[]</cref>
        /// </see>
        ///     that contains the casts
        /// </summary>
        [DataMember]
        public string[] Cast { get; set; }

        /// <summary>
        /// A <see cref="string"/> that contains Classification information
        /// </summary>
        [DataMember]
        public string Classification{ get; set; }

        /// <summary>
        /// A <see cref="string"/> that contains Genre information
        /// </summary>
        [DataMember]
        public string Genre{ get; set; }

        /// <summary>
        /// A <see cref="int"/> that uniquely identifies a movie
        /// </summary>
        [DataMember]
        public int MovieId { get; set; }

        /// <summary>
        /// A <see cref="int"/> that contains rating of a movie
        /// </summary>
        [DataMember]
        public int Rating { get; set; }
        
        /// <summary>
        /// A <see cref="int"/> that contains movie release year
        /// </summary>
        [DataMember]
        public int ReleaseDate { get; set; }

        /// <summary>
        /// A <see cref="string"/> that contains Title/Name of a movie
        /// </summary>
        [DataMember]
        public string Title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesLibrary.Interface;

namespace MoviesLibrary.Provider.Repository
{
    /// <summary>
    /// A <b> Repository </b> for Movies Library
    /// </summary>
    public class MoviesRepository : IRepository<Movie>
    {
        /// <summary>
        /// MoviesDataSorce in DataContext
        /// </summary>
        protected MovieDataSource DataContext { get; private set; }
        
        /// <summary>
        /// Cache to hold movies and do operation
        /// </summary>
        public ICacheProvider Cache { get; set; }

        public MoviesRepository()
            : this(new DefaultCacheProvider())
        {

        }
        public MoviesRepository(ICacheProvider cacheProvider)
        {
            DataContext = new MovieDataSource();
            Cache = cacheProvider;
        }

        /// <summary>
        /// Creates a new movie in movie library by calling MovieDataSource (DataContext)
        /// </summary>
        /// <param name="movie"> the movie to be created</param>
        /// <returns></returns>
        public int Create(Movie movie)
        {
            var maxMovieId = GetAllData().Max(p => p.MovieId);
            var movieId = DataContext.Create(new MovieData
            {
                Cast = movie.Cast,
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = maxMovieId + 1,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            });
            ClearCache();
            return movieId;
        }

        /// <summary>
        /// Get All movies from movie library by calling MovieDataSource (DataContext)
        /// </summary>
        /// <returns>List of Movies from movie library</returns>
        public IEnumerable<Movie> GetAllData()
        {
            if(!Cache.IsSet("movies"))
            {
                var moviesList = from m in DataContext.GetAllData()
                                 select new Movie
                                 {
                                     Cast = m.Cast,
                                     Classification = m.Classification,
                                     Genre = m.Genre,
                                     MovieId = m.MovieId,
                                     Rating = m.Rating,
                                     ReleaseDate = m.ReleaseDate,
                                     Title = m.Title
                                 };
                Cache.Set("movies", moviesList, 30);
                return moviesList.ToList();
            }
            else
            {
                var moviesList = from m in Cache.Get("movies") as IEnumerable<Movie>
                                 select new Movie
                                 {
                                     Cast = m.Cast,
                                     Classification = m.Classification,
                                     Genre = m.Genre,
                                     MovieId = m.MovieId,
                                     Rating = m.Rating,
                                     ReleaseDate = m.ReleaseDate,
                                     Title = m.Title
                                 };

                return moviesList.ToList();
            }

            
        }

        /// <summary>
        /// Get Movie details by MovieId by calling MovieDataSource (DataContext)
        /// </summary>
        /// <param name="movieId">Unique Id</param>
        /// <returns>Movie for passed movieId</returns>
        public Movie GetDataById(int movieId)
        {
            var dsMovie = DataContext.GetDataById(movieId);
            var movie = new Movie
            {
                Cast = dsMovie.Cast,
                Classification = dsMovie.Classification,
                Genre = dsMovie.Genre,
                MovieId = dsMovie.MovieId,
                Rating = dsMovie.Rating,
                ReleaseDate = dsMovie.ReleaseDate,
                Title = dsMovie.Title
            };

            return movie;
        }

        /// <summary>
        /// Update an existing movie details by calling MovieDataSource (DataContext)
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        public void Update(Movie movie)
        {
            DataContext.Update(new MovieData
            {
                Cast = movie.Cast,
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = movie.MovieId,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            });
            ClearCache();
        }

        /// <summary>
        /// Search movies from the store for given criteria by calling MovieDataSource (DataContext)
        /// </summary>
        /// <param name="searchText"> the search criteria</param>
        /// <returns>List of movies matching search criteria</returns>
        public IEnumerable<Movie> SearchMovies(string searchText)
        {
            int searchNumber;
            int.TryParse(searchText, out searchNumber);
            var movies = from movie in GetAllData()
                            where
                            (movie.Title.ToLower().Contains(searchText.ToLower()) ||
                            movie.Classification.ToLower().Contains(searchText.ToLower()) ||
                            movie.Genre.ToLower().Contains(searchText.ToLower()) ||
                            movie.ReleaseDate == searchNumber ||
                            movie.Rating == searchNumber ||
                            movie.Cast.Any(c => c.ToLower().Contains(searchText))
                            )
                            select new Movie
                            {
                                Cast = movie.Cast,
                                Title = movie.Title,
                                Classification = movie.Classification,
                                Genre = movie.Genre,
                                Rating = movie.Rating,
                                ReleaseDate = movie.ReleaseDate,
                                MovieId = movie.MovieId
                            };

            return movies.ToList();
        }

        /// <summary>
        /// Removes the cached movies data
        /// </summary>
        public void ClearCache()
        {
            Cache.Invalidate("movies");
        }

    }
}

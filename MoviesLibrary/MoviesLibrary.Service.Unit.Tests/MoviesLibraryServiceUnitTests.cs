using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using MoviesLibrary.Interface;
using MoviesLibrary.Service.FaultContracts;
using MoviesLibrary.Service.ServiceContracts;
using System.ServiceModel;


namespace MoviesLibrary.Service.Unit.Tests
{
    [TestClass]
    public class MoviesLibraryServiceUnitTests
    {
        private static readonly Movie Movie = new Movie
        {
            Cast = "Test Actor1, Test Actor2".Split(','),
            Classification = "G",
            Genre = "Horror",
            MovieId = 1,
            Rating = 2,
            ReleaseDate = 2010,
            Title = "My name is Hero"
        };

        private static readonly DataContracts.Movie DcMovie = new DataContracts.Movie
        {
            Cast = "Updated Actor1, Updated Actor2".Split(','),
            Classification = "G",
            Genre = "Action",
            MovieId = 1,
            Rating = 2,
            ReleaseDate = 2010,
            Title = "My name is Hero"
        };

        public List<Movie> GetMovieData()
        {
            var movieList = new List<Movie>();
            var movie1 = new Movie
            {
                Cast = "Test Actor1, Test Actor2".Split(','),
                Classification = "G",
                Genre = "Horror",
                MovieId = 1,
                Rating = 2,
                ReleaseDate = 2010,
                Title = "My name is Hero"
            };
            movieList.Add(movie1);

            var movie2 = new Movie
            {
                Cast = "Test Actor3, Test Actor4".Split(','),
                Classification = "PG",
                Genre = "Action",
                MovieId = 2,
                Rating = 3,
                ReleaseDate = 2010,
                Title = "Sample Movie"
            };
            movieList.Add(movie2);
            return movieList;
        }

        [TestMethod]
        public void GetMovieById_Scucceeds()
        {
            // Arrange
            var provider = A.Fake<IMovieLibraryProvider>();
            var movieService = new MoviesLibraryService(provider);
            var request = new GetMovieByIdRequest
            {
                MovieId = 1
            };
            //set expectations
            A.CallTo(() => provider.GetDataById(1)).WithAnyArguments().Returns(Movie);
            
            // Act
            var response = movieService.GetMovieById(request);
            A.CallTo(() => provider.GetDataById(1)).WithAnyArguments().MustHaveHappened();

            //Asert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Movie.MovieId);
        }

        [TestMethod]
        public void GetMovieById_Exception_GeneralExceptionMapsToFault()
        {
            // Arrange
            var provider = A.Fake<IMovieLibraryProvider>();
            var movieService = new MoviesLibraryService(provider);
            var request = new GetMovieByIdRequest
            {
                MovieId = 1
            };

            //set expectations
            A.CallTo(() => provider.GetDataById(1)).WithAnyArguments().Returns(Movie);
            A.CallTo(() => provider.GetDataById(1)).WithAnyArguments().Throws(new ArgumentException());

            // Act
            var fault = ExceptionAssert.Throws<FaultException<SystemFault>>(() => movieService.GetMovieById(request));

            //Asert
            Assert.IsNotNull(fault);
        }

        [TestMethod]
        public void GetAllMovies_Scucceeds()
        {
            var provider = A.Fake<IMovieLibraryProvider>();
            var fakesMoviesList = GetMovieData();
            A.CallTo(() => provider.GetAllData()).Returns(GetMovieData());

            var movieService = new MoviesLibraryService(provider);
            var response = movieService.GetAllMovies();

            A.CallTo(() => provider.GetAllData()).MustHaveHappened();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Movies);
            Assert.AreEqual(fakesMoviesList.Count, response.Movies.Count);
        }

        [TestMethod]
        public void UpdateMovie_Scucceeds()
        {
            var provider = A.Fake<IMovieLibraryProvider>();
            
            A.CallTo(() => provider.Update(Movie));

            var request = new UpdateMovieRequest
            {
                Movie = DcMovie
            };
            var movieService = new MoviesLibraryService(provider);
            var response = movieService.UpdateMovie(request);

            A.CallTo(() => provider.Update(Movie)).WithAnyArguments().MustHaveHappened();
            Assert.IsNotNull(response);
            Assert.AreEqual(response.MovieId, Movie.MovieId);
        }

        [TestMethod]
        public void CreateMovie_Scucceeds()
        {
            var provider = A.Fake<IMovieLibraryProvider>();

            A.CallTo(() => provider.Create(Movie));

            var request = new CreateMovieRequest
            {
                Movie = DcMovie
            };
            var movieService = new MoviesLibraryService(provider);
            var response = movieService.CreateMovie(request);

            A.CallTo(() => provider.Create(Movie)).WithAnyArguments().MustHaveHappened();
            Assert.IsNotNull(response);
            Assert.AreEqual(response.MovieId, Movie.MovieId);
        }
    }
}

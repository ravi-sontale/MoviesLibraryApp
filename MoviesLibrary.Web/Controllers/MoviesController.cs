using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using MoviesLibrary.Web.Models;
using MoviesLibrary.Web.MoviesLibraryService;
using System.Web.Mvc.Async;
using log4net;

namespace MoviesLibrary.Web.Controllers
{
    public class MoviesController : AsyncController
    {
        public const string Message = "Could not perform current operation. Please try again later.";
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        //Async Method for retrieving all movies
        public void GetAllMoviesAsync()
        {
            try
            {
                AsyncManager.OutstandingOperations.Increment();

                Task.Factory.StartNew(() =>
                {
                    using (var proxy = new MoviesLibraryServiceClient())
                    {
                        var movies = proxy.GetAllMovies();
                        AsyncManager.Parameters["result"] = movies;
                        AsyncManager.OutstandingOperations.Decrement();
                    }
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[MoviesController - GetAllMoviesAsync] Unable to retrieve all movies from library due to following error: {0}", ex.InnerException);
                ViewBag.Message = Message;
            }
        }

        public ActionResult GetAllMoviesCompleted(List<Movie> result)
        {
            var list = new List<MovieModel>();
            if (result != null)
            {
                list = result.Select(m => new MovieModel
                {
                    Cast = m.Cast.ToArray(),
                    Classification = m.Classification,
                    Title = m.Title,
                    Genre = m.Genre,
                    MovieId = m.MovieId,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate
                }).ToList();
            }
            return View("List", list);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (!(filterContext.Exception is TimeoutException) || !(filterContext.Controller is AsyncController))
                return;
            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller", "Movies"},
                    {"Action", "TimeoutRedirect"}
                });
            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }

        public ActionResult TimeoutRedirect()
        {
            return View("Error");
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View("Add");
        }

        [HttpPost]
        public ActionResult AddMovie(MovieModel model, FormCollection collection)
        {
            var cast = collection.Get("Cast");
            if (ModelState.IsValid)
            {
                try
                {
                    using (var proxy = new MoviesLibraryServiceClient())
                    {
                        proxy.CreateMovie(new Movie
                        {
                            Title = model.Title,
                            Classification = model.Classification,
                            Genre = model.Genre,
                            Rating = Convert.ToInt32(model.Rating),
                            ReleaseDate = Convert.ToInt32(model.ReleaseDate),
                            Cast = cast.Split(',').ToList()
                        });
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("[MoviesController - AddMovie] Unable to add new movie to library due to following error: {0}", ex.InnerException);
                    ViewBag.Message = Message;
                }
                
                return RedirectToAction("GetAllMovies", "Movies");
            }

            return View("Add");
        }

        [HttpGet]
        public ActionResult EditMovie(int movieId, string title, string classification, string genre, int rating, int releaseDate, string cast)
        {
            var model = new MovieModel
            {
                MovieId = movieId,
                Cast = cast.Split(','),
                Title = title,
                Classification = classification,
                Genre = genre,
                Rating = rating,
                ReleaseDate = releaseDate

            };
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult EditMovie(MovieModel model, FormCollection collection)
        {
            var cast = collection.Get("Cast");
            if (ModelState.IsValid)
            {
                try
                {
                    using (var proxy = new MoviesLibraryServiceClient())
                    {
                        proxy.UpdateMovie(new Movie
                        {
                            Title = model.Title,
                            Classification = model.Classification,
                            Genre = model.Genre,
                            Rating = model.Rating,
                            ReleaseDate = model.ReleaseDate,
                            MovieId = model.MovieId,
                            Cast = cast.Split(',').ToList(),
                        });
                        return RedirectToAction("GetAllMovies", "Movies");
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("[MoviesController - EditMovie] Unable to edit movie due to following error: {0}", ex.InnerException);
                    ViewBag.Message = Message;
                }
                
            }
            return View("Edit");
        }

        [HttpPost]
        public ActionResult SearchMovie(FormCollection collection)
        {
            var searchText = collection.Get("SearchText");
            var list = new List<MovieModel>();
            try
            {
                using (var proxy = new MoviesLibraryServiceClient())
                {
                    var movies = proxy.SearchMovies(searchText);
                    if (movies != null)
                    {
                        list = movies.Select(m => new MovieModel
                        {
                            Cast = m.Cast.ToArray(),
                            Classification = m.Classification,
                            Title = m.Title,
                            Genre = m.Genre,
                            MovieId = m.MovieId,
                            Rating = m.Rating,
                            ReleaseDate = m.ReleaseDate
                        }).ToList();
                    }
                    else
                    {
                        ViewBag.Message = "There are no movies matching search criteria";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("[MoviesController - AddMovie] Unable to retrieve movies with search criteria [{0}] from library due to following error: {0}", searchText, ex.InnerException);
                ViewBag.Message = Message;
            }
            
            return View("List", list);
        }
    }
}

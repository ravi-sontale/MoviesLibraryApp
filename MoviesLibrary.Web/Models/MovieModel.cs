using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MoviesLibrary.Web.Models
{
    public class MovieModel
    {
        public string[] Cast { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int MovieId { get; set; }
        public int Rating { get; set; }
        [Required]
        public int ReleaseDate { get; set; }
        [Required]
        public string Title { get; set; }
        public string SearchText { get; set; }
    }
}
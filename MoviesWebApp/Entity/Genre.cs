using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Entity
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}

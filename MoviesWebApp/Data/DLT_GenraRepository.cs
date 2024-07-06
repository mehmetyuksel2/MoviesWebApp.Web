/*using MoviesWebApp.Entity;
using MoviesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.Data
{
    public class DLT_GenraRepository
    {
        private static readonly List<Genre> _genres = null;

        static DLT_GenraRepository()
        {

            _genres = new List<Genre>()
            {
                new Genre { GenreId = 1, name="macera" },
                new Genre { GenreId = 2, name="komedi" },
                new Genre { GenreId = 3, name="romantik" },
                new Genre { GenreId = 4, name="savaş" },
                new Genre { GenreId = 5, name="bilim kurgu" }
            };
        }
        public static List<Genre> Genres
        {
            get
            {
                return _genres;
            }
        }
        public static void Add(Genre genre)
        {

            _genres.Add(genre);
        }

        public static Genre GetById(int id)
        {
            return _genres.FirstOrDefault(g => g.GenreId == id);
        }
    }
}
*/
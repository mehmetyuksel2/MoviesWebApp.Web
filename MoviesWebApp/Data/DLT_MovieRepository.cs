//using AspNetCore;
/*using MoviesWebApp.Entity;
using MoviesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.Data
{
    public class DLT_MovieRepository
    {
        private static readonly List<Movie> _movies = null;

        static DLT_MovieRepository()
        {
            _movies = new List<Movie>()
            {
                new Movie {
                    MovieId = 1,
                    Title = "Taşıyıcı",
                    Description = "özel taşıyıcılık yapan bir şöförün akıl almaz maceraları",
                    ImageUrl = "1.jpg",
                    GenreId = 1
                },
                new Movie {
                    MovieId = 2,
                    Title = "harry potter",
                    Description = "sihir okulunda yaşanan bir birinden heyecanlı olaylar serisi",
                    ImageUrl = "2.jpg",
                    GenreId = 1
                },
                new Movie {
                    MovieId = 3,
                    Title = "Baba parası",
                    Description = "dedelerinden kalan mirasın şifresini ararken bin bir maceraya atılan bir topluluk",
                    ImageUrl = "3.jpg",
                    GenreId = 3
                },
                new Movie {
                    MovieId = 4,
                    Title = "recep ivedik",
                    Description = "yalnız ve arakadaşı olmayan kaba bir insanın komik yaşam öyküsü",
                    ImageUrl = "1.jpg",
                    GenreId = 3
                },
                new Movie {
                    MovieId = 5,
                    Title = "senede bir gün",
                    Description = "kavuşamayan iki sevgilinin senede bir kere buluştuğu dram filmi",
                    ImageUrl = "2.jpg",
                    GenreId = 3
                },
                new Movie {
                    MovieId = 6,
                    Title = "300 spartalı",
                    Description = "yalnız 300 askerin yüz binlerce düşman askerine karşı vermiş olduğu mücadeleyi anlatan film",
                    ImageUrl = "3.jpg",
                    GenreId = 4
                }
            };

        }
        public static List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }

        public static void Add(Movie movie)
        {
            movie.MovieId = _movies.Count() + 1;
            _movies.Add(movie);
            
        }
        public static Movie GetById(int id) {

            return _movies.FirstOrDefault(m => m.MovieId == id);
        }
        public static void Edit(Movie m)
        {
            foreach (var movie in _movies) { 
            
                if(movie.MovieId == m.MovieId)
                {
                    movie.Title = m.Title;
                    movie.Description = m.Description;
                    movie.ImageUrl = m.ImageUrl;
                    movie.GenreId = m.GenreId;
                    break;
                }

            }
        }
        public static void Delete(int MovieId) {

            var movie = GetById(MovieId);
            if (movie != null)
            {
                _movies.Remove(movie);
            }
        }

    }
}
*/
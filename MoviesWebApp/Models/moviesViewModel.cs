using MoviesWebApp.Entity;
using System.Collections.Generic;

namespace MoviesWebApp.Models
{
    public class moviesViewModel//tüm listler bir paket altında toplanıyor
    {
        public List<Movie> Movies { get; set; }
    }
}

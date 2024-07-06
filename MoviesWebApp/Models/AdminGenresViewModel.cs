using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class AdminGenresViewModel
    {
        [Required(ErrorMessage ="Tür bilgisi girmelisiniz")]
        public string Name { get; set; }
        public List<AdminGenreViewModel> Genres { get; set; }
    }
    public class AdminGenreViewModel()
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<AdminGenreViewModel> Genres { get; internal set; }
    }
    public class AdminGenreEditViewModel()
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage ="Tür Bilgisi girmelisiniz")]
        public string Name { get; set; }
        public List<AdminMovieViewModel> Movies { get; set; }
    }
}

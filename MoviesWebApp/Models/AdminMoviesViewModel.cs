using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Entity;
using MoviesWebApp.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class AdminMoviesViewModel
    {
        //public List<Movie> Movies { get; set; }//movie modelinden direkt çekilen List model
        public List<AdminMovieViewModel> Movies { get; set; }
    }
    public class AdminMovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<Genre> Genres { get; set; }
    }
    public class AdminMovieCreateModel
    {
        [Display(Name = "Film Adı")]
        [Required(ErrorMessage ="Film Adı Girmelisiniz")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="Film Adı İçin 3-50 Karakter Girmelisiniz")]
        [Remote(action:"VerifyMovie", controller:"admin")]//bu satır title inputunu uzaktan sayfa yenilenmeden kontrol etmesi için eklenmiştir
        public string Title { get; set; }
        [Display(Name = "Film Açıklaması")]
        [Required(ErrorMessage = "Film Açıklaması Girmelisiniz")]
        [StringLength(3000, MinimumLength = 3, ErrorMessage = "Film Açıklaması İçin 10-3000 Karakter Girmelisiniz")]
        public string Description { get; set; }
        [ClassicMovie(1950)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public bool IsClassic { get; set; }
        [Required(ErrorMessage = "En az bir tür seçmelisiniz")]
        public int[] GenreIds { get; set; }
    }
    public class AdminEditMovieViewModel
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Film Adı Girmelisiniz")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Film Adı İçin 3-50 Karakter Girmelisiniz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Film Açıklaması Girmelisiniz")]
        [StringLength(3000, MinimumLength = 3, ErrorMessage = "Film Açıklaması İçin 10-3000 Karakter Girmelisiniz")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "En az bir tür seçmelisiniz")]
        public int[] GenreIds { get; set; }
    }
}

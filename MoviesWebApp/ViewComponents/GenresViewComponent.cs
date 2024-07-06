using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.ViewComponents
{
    public class GenresViewComponent : ViewComponent
    {
        private readonly MovieContext _context;

        public GenresViewComponent(MovieContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData.Values["id"];
            return View(_context.Genres.ToList());
        }
    }
}

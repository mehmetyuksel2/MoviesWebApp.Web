using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.Controllers


{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()// Bu actionda değişkenlere değerler atanıp bu değerleri movie classında tanımlanan değişkenlere atanıyor.
        {
            var model = new HomePageViewModel
            {
                PopularMovies = _context.MovieSet.ToList()
            };


            return View(model);//action adı Index olduğu için adıyla ilişkili cshtml dosyasına içinde "m" değişkenleriyle beraber yönlendirir.
        }
        public IActionResult About()
        {
            
            return View();//action adı Index olduğu için adıyla ilişkili cshtml dosyasına yani about.cshtml dosyasına yönlendirir
        }
    }
}

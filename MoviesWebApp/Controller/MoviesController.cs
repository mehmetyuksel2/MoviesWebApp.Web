using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Entity;
using MoviesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MoviesWebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }


        //localhost:5000/movies/
        public string Index()
        {
            return "film anasayfa";
        }



        //localhost:5000/movies/list
        //localhost:5000/movies/list/1
        [HttpGet]
        public IActionResult List(int? id, string q) // parametre gönderilebilir bir metod. gönderilmezse (?) ile boş kalabilir ibaresi mevcut
        {
            // (controller) / (action) / id?
            //localhost:5000/movies/list/2

            //var controller = RouteData.Values["controller"]; 
            //var action = RouteData.Values["action"];
            //var genreid = RouteData.Values["id"];
            //var kelime = HttpContext.Request.Query["q"].ToString()  //get olarak gönderdiğimiz q değişkeninin değerini çekiyoruz
            //var movies = MovieRepository.Movies;//movieRepository classının altındaki movies metodunu var movies değişkenine atadık.
            
            
            var movies = _context.MovieSet.AsQueryable();
            if (id != null)//id boş değilse
            {
                movies = movies.Include(m=> m.Genres).Where(m => m.Genres.Any(g => g.GenreId == id));//genreid ile ilişkili bütün filmleri liste halinde moviese atadık.
            }
            if(!string.IsNullOrEmpty(q)) {

                movies = movies.Where(i =>
                i.Title.ToLower().Contains(q.ToLower()) //gönderilen arama kelimesinin başlıkta var ise liste olarak ekle
                || i.Description.ToLower().Contains(q.ToLower()));//gönderilen arama kelimesinin açıklamada var ise liste olarak ekle
                //contains içerme manası içerir. aramayı yapan metoddur
            }

            var model = new moviesViewModel()
            {
                Movies = movies.ToList()

            };

            return View("movies", model);//movies.cshtml dosyasına yönlendir ve ikinci parametre ile modeli gönder.
        }

        //localhost:5000/movies/detail
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_context.MovieSet.Find(id));//select sorgusunu gönderir
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "name");// tür bilgilerini list objesi olarak forma Viewbag olarak gönderir
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie m) // buradaki parametre create formundaki namelerle aynı olduğu için otomatik olarak aktarılır
        {
            if (ModelState.IsValid)//girilen inputların zorunlu olanları kontrol eder
            {
                //model binding aracılığı ile create formundaki verileri kontrol eder ve name ile verileri çeker

                //MovieRepository.Add(m);
                _context.MovieSet.Add(m);//insert sorgusuna karşılık gelir
                _context.SaveChanges();//sorguyu gönderir
                TempData["Message"] = $"{m.Title} Film Eklendi";

                return RedirectToAction("List");//ekleme sonrası farklı actiona yönlendirme satırı

            }
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "name");// tür bilgilerini list objesi olarak forma Viewbag olarak gönderir
            return View();
          
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "name");// tür bilgilerini list objesi olarak forma Viewbag olarak gönderir
            return View(_context.MovieSet.Find(id));
        }
        [HttpPost]
        public IActionResult edit(Movie m)
        {
            if (ModelState.IsValid)//girilen inputların zorunlu olanları kontrol eder
            {
                //MovieRepository.Edit(m);// repository dosyasından veriler güncellenmekte
                _context.MovieSet.Update(m);
                _context.SaveChanges();
                // /movies/details/1
                return RedirectToAction("Details", "Movies", new { @id = m.MovieId });//aksiyon olarak yönlendiriliyor Movies/Details/id?

            }
            ViewBag.Genres = new SelectList(_context.MovieSet.ToList(), "GenreId", "name");// tür bilgilerini list objesi olarak forma Viewbag olarak gönderir
            return View(m);
        }
        [HttpPost]
        public IActionResult Delete(int MovieId, string Title)
        {
            //MovieRepository.Delete(MovieId);
            var entity = _context.MovieSet.Find(MovieId);
            _context.MovieSet.Remove(entity);
            _context.SaveChanges();
            TempData["Message"] = $"{Title} Film Silindi";
            return RedirectToAction("List");
        }
    }
}

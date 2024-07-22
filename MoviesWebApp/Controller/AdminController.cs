using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Entity;
using MoviesWebApp.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp.Controller2
{
    public class AdminController : Controller
    {
        private readonly MovieContext _context;
        public AdminController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MovieList()
        {
            return View(new AdminMoviesViewModel
            {
                //Movies = _context.MovieSet.Include(g=>g.Genres).ToList(); // genres sütunların tümünü çeken kod bloğu
                Movies = _context.MovieSet
                .Include(g => g.Genres)
                .Select(m => new AdminMovieViewModel
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ImageUrl = m.ImageUrl,
                    Genres = m.Genres.ToList()
                })
                .ToList()
            });
        }

        public IActionResult MovieUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _context.MovieSet.Select(m => new AdminEditMovieViewModel
            {
                MovieId = m.MovieId,
                Title = m.Title,
                ImageUrl = m.ImageUrl,
                Description = m.Description,
                GenreIds = m.Genres.Select(i => i.GenreId).ToArray()
            }).FirstOrDefault(m => m.MovieId == id);

            ViewBag.Genres = _context.Genres.ToList();

            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> MovieUpdate(AdminEditMovieViewModel model, int[] genreId, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var entity = _context.MovieSet.Include("Genres").FirstOrDefault(i => i.MovieId == model.MovieId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Title = model.Title;
                entity.Description = model.Description;
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);//.jpg, .png
                    var fileName = string.Format($"movies{Guid.NewGuid()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                    entity.ImageUrl = fileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                entity.Genres = model.GenreIds.Select(id => _context.Genres.FirstOrDefault(i => i.GenreId == id)).ToList();

                _context.SaveChanges();
                return RedirectToAction("MovieList");

            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }
        public IActionResult GenreUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _context
                .Genres
                .Select(g => new AdminGenreEditViewModel
                {
                    GenreId = g.GenreId,
                    Name = g.name,
                    Movies = g.Movies.Select(i => new AdminMovieViewModel
                    {
                        MovieId = i.MovieId,
                        Title = i.Title,
                        ImageUrl = i.ImageUrl,

                    }).ToList()
                }).FirstOrDefault(g => g.GenreId == id);

            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public IActionResult GenreDelete(int genreId)//tür silindi
        {
            var entity = _context.Genres.Find(genreId);
            if (entity != null)
            {
                _context.Genres.Remove(entity);
                _context.SaveChanges();
            }

            return RedirectToAction("GenreList");
        }
        [HttpPost]
        public IActionResult MovieDelete(int movieId)
        {
            var entity = _context.MovieSet.Find(movieId);
            if (entity != null)
            {
                _context.MovieSet.Remove(entity);
                _context.SaveChanges();
            }
            return RedirectToAction("MovieList");
        }
        [HttpPost]
        public IActionResult GenreUpdate(AdminGenreEditViewModel model, int[] movieIds)
        {

            if (ModelState.IsValid)
            {

                var entity = _context.Genres.Include("Movies").FirstOrDefault(i => i.GenreId == model.GenreId);
                //genres tablosunu ilgli olan movies verileriyle beraber çeker
                if (entity == null)
                {
                    return NotFound();
                }
                entity.name = model.Name;//genre ismi güncelleriz
                foreach (var id in movieIds)
                {
                    entity.Movies.Remove(entity.Movies.FirstOrDefault(m => m.MovieId == id));
                    //formdaki checkbox idlerini birer birer kontrol edip
                    //eşleşenler movie ve genre ortak tablodan silinir
                }

                _context.SaveChanges();
                return RedirectToAction("GenreList");
                //genreliste geri döndürür
            }
            return View(model);//form şartlarını sağlanmaması durumunda tekrar view e model ile beraber yönlendiririz
        }
        public IActionResult MovieCreate()
        {
            ViewBag.Genres = _context.Genres.ToList();//tür listesini viewbag.genrese aktarırız
            return View(new AdminMovieCreateModel());//boş bir AdminMovieCreateModel objesi ile viewe döneriz
        }
        [HttpPost]
        public IActionResult MovieCreate(AdminMovieCreateModel model)
        {
            if (model.Title != null && model.Title.Contains("@"))//@ işareti olup olmadığını kontrol eder
            {
                ModelState.AddModelError("", "Film başlığında '@' işareti kullanamazsınız.");
            }
            //if (model.GenreIds == null) 
            //{
            //    ModelState.AddModelError("GenreIds", "En az bir tür seçmeniz gerekmektedir");

            //}
            if (ModelState.IsValid)
            {
                var entity = new Movie
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = "no-image.jpg"
                };
                //entity.Genres = new List<Genre>();//neden boş bir nesne oluşturulması gerekiyor ?
                foreach (var id in model.GenreIds)
                {

                    entity.Genres.Add(_context.Genres.FirstOrDefault(i => i.GenreId == id));
                }
                _context.MovieSet.Add(entity);
                _context.SaveChanges();
                return RedirectToAction("MovieList", "Admin");
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult GenreCreate(AdminGenresViewModel model)
        {
            if (model.Name != null && model.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "Tür adı minimum 3 karakterli olmalıdır");
            }
            if (ModelState.IsValid)
            {
                _context.Genres.Add(new Genre { name = model.Name });
                _context.SaveChanges();

                return RedirectToAction("GenreList");
            }
            return View("GenreList", GetGenre());
        }
        private AdminGenresViewModel GetGenre()
        {
            return new AdminGenresViewModel
            {
                Genres = _context.Genres.Select(g => new AdminGenreViewModel
                {
                    GenreId = g.GenreId,
                    Name = g.name,
                    Count = g.Movies.Count
                }).ToList()
            };
        }
        public IActionResult GenreList()
        {
            return View(GetGenre());
        }

        public IActionResult VerifyMovie(string title)//fil adı kontrolü
        {
            if (_context.MovieSet.Any(i=>i.Title == title))
            {
                return Json($"Zaten {title} başlığı başka bir filmde kullanılıyor");
            }
            return Json(true);
        }
    }
}

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
    public class UserController : Controller
    {
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            return View();
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyUserName(string userName)
        {
            var users = new List<string> { "mehmetyuksel", "denemeisim" };
            if (users.Any(i => i == userName))
            {
                return Json($"zaten bu {userName} adı daha önce alınmış");

            }

            return Json(true);


        }
    }
}

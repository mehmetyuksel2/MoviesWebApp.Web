using Microsoft.AspNetCore.Mvc;

namespace MoviesWebApp.Controllers
{
    public interface IHomeController
    {
        IActionResult About();
        IActionResult Index();
    }
}
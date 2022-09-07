using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult Team()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            return View();
        }

    }
}

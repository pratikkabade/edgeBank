using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;

namespace frontend.Controllers
{
    public class ErrorController : Controller
    {

        [Route("{*url}", Order = 999)]
        public IActionResult Error404()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            Response.StatusCode = 404;
            return View("Error404");
        }

        public IActionResult Error401()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            Response.StatusCode = 401;
            return View();
        }

    }
}

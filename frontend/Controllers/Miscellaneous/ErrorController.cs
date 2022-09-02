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
            Response.StatusCode = 404;
            return View("Error404");
        }

        public IActionResult Error401()
        {
            Response.StatusCode = 401;
            return View();
        }

    }
}

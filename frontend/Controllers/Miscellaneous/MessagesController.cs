using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;

namespace frontend.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult CreateMessage()
        {
            return View();
        }

    }
}

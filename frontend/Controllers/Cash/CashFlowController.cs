using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;

namespace frontend.Controllers
{
    public class CashFlowController : Controller
    {
        public IActionResult CreateMessage()
        {
            return View();
        }

        public IActionResult EditMessage()
        {
            return View();
        }

        public IActionResult DeleteMessage()
        {
            return View();
        }

        public IActionResult SentMessage()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using frontend.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace frontend.Controllers
{
    public class UserController : Controller
    {
        private readonly static HttpClient httpClient = new();
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users user)
        {
            if (user.Email == null || user.Password == null)
            {
                return View("Login");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, Configuration.GetValue<string>("WebAPIBaseUrl") + "/authenticate");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user.Email}:{user.Password}")));

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadAsStringAsync().Result;
                JWT jwt = JsonConvert.DeserializeObject<JWT>(token);
                HttpContext.Session.SetString("token", jwt.Token);
                HttpContext.Session.SetString("UserName", user.Email);

                ViewBag.LogMessage = HttpContext.Session.GetString("UserName");

                if (user.Role == "Admin")
                {
                    return RedirectToAction("MyAccount", "User");
                }
                else
                {
                    return RedirectToAction("MyAccount", "User");
                }
            }
            ViewBag.Message = "Invalid Username or Password";
            return View("Login");
        }

        public IActionResult MyAccount()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            return View();
        }

    }
}

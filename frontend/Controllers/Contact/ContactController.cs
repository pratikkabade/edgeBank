using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using frontend.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace frontend.Controllers
{

    public class ContactController : Controller
    {
        private static HttpClient httpMsgClient = new HttpClient();
        public ContactController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }



        //CREATE
        [HttpGet]
        public async Task<IActionResult> ContactUs()
        {
            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");
            await Task.Delay(1000);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(Contact contactMsg)
        {
            if (ModelState.IsValid)
            {
                var serializedProductToCreate = JsonConvert.SerializeObject(contactMsg);
                var request = new HttpRequestMessage(HttpMethod.Post, Configuration.GetValue<string>("WebAPIBaseUrl") + "/contact");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedProductToCreate);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpMsgClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SentMessage", "Messages");
                }
                else
                {
                    return RedirectToAction("Error401", "Error");
                }
            }
            else
                return RedirectToAction("Error404", "Error");
        }



        //INDEX
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            httpMsgClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpMsgClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = await httpMsgClient.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + "/contact");
            var content = await response.Content.ReadAsStringAsync();

            ViewBag.LogMessage = HttpContext.Session.GetString("UserName");

            if (response.IsSuccessStatusCode)
            {
                var contact = new List<Contact>();
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    contact = JsonConvert.DeserializeObject<List<Contact>>(content);
                }
                return View(contact);
            }
            else
            {
                return RedirectToAction("Error401", "Error");
            }
        }

    }
}

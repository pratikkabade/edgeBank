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
    public class NewUserController : Controller
    {

        private static HttpClient http_Client = new HttpClient();

        public NewUserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            http_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http_Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = await http_Client.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + "/administration");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var user = new List<Users>();
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    user = JsonConvert.DeserializeObject<List<Users>>(content);
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Error401", "Error");
            }
        }


        //CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Task.Delay(1000);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Users new_user)
        {
            if (ModelState.IsValid)
            {
                var serializedProductToCreate = JsonConvert.SerializeObject(new_user);
                var request = new HttpRequestMessage(HttpMethod.Post, Configuration.GetValue<string>("WebAPIBaseUrl") + "/administration");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedProductToCreate);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await http_Client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CreateMessage", "Messages");
                }
                else
                {
                    // ViewBag.Message = "Admin access required";
                    // return View("Create");
                    return RedirectToAction("Error401", "Error");
                }
            }
            else
                return RedirectToAction("Error401", "Error");
        }





        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await http_Client.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + $"/administration/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var new_user = new Users();
            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                new_user = JsonConvert.DeserializeObject<Users>(content);
            }
            return View(new_user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Users new_user)
        {
            if (ModelState.IsValid)
            {
                //http_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http_Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var serializedProductToEdit = JsonConvert.SerializeObject(new_user);
                var request = new HttpRequestMessage(HttpMethod.Put, Configuration.GetValue<string>("WebAPIBaseUrl") + $"/administration/{new_user.Id}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedProductToEdit);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await http_Client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EditMessage", "Messages");
                }
                else
                {
                    // ViewBag.Message = "Admin access required";
                    // return View("Edit");
                    return RedirectToAction("Error401", "Error");
                }
            }
            else
                return RedirectToAction("Error401", "Error");
        }



        //DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await http_Client.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + $"/administration/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var new_user = new Users();
            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                new_user = JsonConvert.DeserializeObject<Users>(content);
            }
            return View(new_user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Users new_user)
        {
            if (ModelState.IsValid)
            {
                //http_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http_Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var serializedProductToDelete = JsonConvert.SerializeObject(new_user);
                var request = new HttpRequestMessage(HttpMethod.Delete, Configuration.GetValue<string>("WebAPIBaseUrl") + $"/administration/{new_user.Id}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedProductToDelete);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await http_Client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("DeleteMessage", "Messages");
                }
                else
                {
                    // ViewBag.Message = "Admin access required";
                    // return View("Delete");
                    return RedirectToAction("Error401", "Error");
                }
            }
            else
                return RedirectToAction("Error401", "Error");
        }

    }
}

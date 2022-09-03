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




        // //EDIT
        // [HttpGet]
        // public async Task<IActionResult> Edit(int id)
        // {
        //     var response = await httpMsgClient.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + $"/contact/{id}");
        //     response.EnsureSuccessStatusCode();
        //     var content = await response.Content.ReadAsStringAsync();
        //     var newMsg = new Contact();
        //     if (response.Content.Headers.ContentType.MediaType == "application/json")
        //     {
        //         newMsg = JsonConvert.DeserializeObject<Contact>(content);
        //     }
        //     return View(newMsg);
        // }

        // [HttpPost]
        // public async Task<IActionResult> Edit(Contact newMsg)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         //httpMsgClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         httpMsgClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
        //         var serializedProductToEdit = JsonConvert.SerializeObject(newMsg);
        //         var request = new HttpRequestMessage(HttpMethod.Put, Configuration.GetValue<string>("WebAPIBaseUrl") + $"/contact/{newMsg.Id}");
        //         request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         request.Content = new StringContent(serializedProductToEdit);
        //         request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //         var response = await httpMsgClient.SendAsync(request);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             return RedirectToAction("SentMessage", "Messages");
        //         }
        //         else
        //         {
        //             // ViewBag.Message = "Admin access required";
        //             // return View("Edit");
        //             return RedirectToAction("Error401", "Error");
        //         }
        //     }
        //     else
        //         return RedirectToAction("Error401", "Error");
        // }





        // //DELETE
        // [HttpGet]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var response = await httpMsgClient.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + $"/contact/{id}");
        //     response.EnsureSuccessStatusCode();
        //     var content = await response.Content.ReadAsStringAsync();
        //     var contactMsg = new Contact();
        //     if (response.Content.Headers.ContentType.MediaType == "application/json")
        //     {
        //         contactMsg = JsonConvert.DeserializeObject<Contact>(content);
        //     }
        //     return View(contactMsg);
        // }

        // [HttpPost]
        // public async Task<IActionResult> Delete(Contact contactMsg)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         //httpMsgClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         httpMsgClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
        //         var serializedProductToDelete = JsonConvert.SerializeObject(contactMsg);
        //         var request = new HttpRequestMessage(HttpMethod.Delete, Configuration.GetValue<string>("WebAPIBaseUrl") + $"/contact/{contactMsg.Id}");
        //         request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         request.Content = new StringContent(serializedProductToDelete);
        //         request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //         var response = await httpMsgClient.SendAsync(request);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             return RedirectToAction("SentMessage", "Messages");
        //         }
        //         else
        //         {
        //             // ViewBag.Message = "Admin access required";
        //             // return View("Delete");
        //             return RedirectToAction("Error401", "Error");
        //         }
        //     }
        //     else
        //         ViewBag.Message = "wrong";
        //     return View("Delete");
        // }
    }
}

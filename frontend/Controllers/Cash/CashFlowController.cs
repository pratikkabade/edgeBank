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

    public class CashFlowController : Controller
    {
        //INDEX
        private static HttpClient cashClient = new HttpClient();
        public CashFlowController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // GET ALL
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            cashClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            cashClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = await cashClient.GetAsync(Configuration.GetValue<string>("WebAPIBaseUrl") + "/CashFlow/");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var transaction = new List<CashFlow>();
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    transaction = JsonConvert.DeserializeObject<List<CashFlow>>(content);
                }
                return View(transaction);
            }
            else
            {
                return RedirectToAction("Error401", "Error");
            }
        }
    }
}

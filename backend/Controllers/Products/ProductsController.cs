using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private DataBaseContext data_context;
        public ProductsController(DataBaseContext data_context)
        {
            this.data_context = data_context;
        }

        // INDEX
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return data_context.Products.ToList();
        }

        // DETAILS
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return this.data_context.Products.Where(product => product.Id == id).FirstOrDefault();
        }

        // CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public string Post([FromBody] Product product)
        {
            this.data_context.Products.Add(product);
            this.data_context.SaveChanges();
            return "Product created successfully!";
        }

        // EDIT
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            this.data_context.Products.Update(product);
            this.data_context.SaveChanges();
        }


        // DELETE
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.data_context.Products.Remove(this.data_context.Products.Where(product => product.Id == id).FirstOrDefault());
            this.data_context.SaveChanges();
        }
    }
}

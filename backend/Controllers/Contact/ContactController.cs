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
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private DataBaseContext msg_context;
        public ContactController(DataBaseContext msg_context)
        {
            this.msg_context = msg_context;
        }

        // INDEX
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return msg_context.ContactMsg.ToList();
        }

        // CREATE
        [HttpPost]
        public string Post([FromBody] Contact message)
        {
            this.msg_context.ContactMsg.Add(message);
            this.msg_context.SaveChanges();
            return "Message sent successfully!";
        }

        // DETAILS
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public Contact Get(int id)
        {
            return this.msg_context.ContactMsg.Where(contact => contact.Id == id).FirstOrDefault();
        }

        // DELETE
        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.msg_context.ContactMsg.Remove(this.msg_context.ContactMsg.Where(contact => contact.Id == id).FirstOrDefault());
            this.msg_context.SaveChanges();
        }
    }
}

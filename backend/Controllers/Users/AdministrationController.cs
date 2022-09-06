using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdministrationController : Controller
    {
        private DataBaseContext user_data_context;
        public AdministrationController(DataBaseContext user_data_context)
        {
            this.user_data_context = user_data_context;
        }


        // CREATE, EDIT, DELETE OPERATIONS
        // INDEX
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return user_data_context.User.ToList();
        }

        // DETAILS
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public Users Get(int id)
        {
            return this.user_data_context.User.Where(user => user.UserId == id).FirstOrDefault();
        }

        // CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public string Post([FromBody] Users New_User)
        {
            this.user_data_context.User.Add(New_User);
            this.user_data_context.SaveChanges();
            return "New_User created successfully!";
        }


        // EDIT
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users New_User)
        {
            this.user_data_context.User.Update(New_User);
            this.user_data_context.SaveChanges();
        }

        // DELETE
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.user_data_context.User.Remove(this.user_data_context.User.Where(New_User => New_User.UserId == id).FirstOrDefault());
            this.user_data_context.SaveChanges();
        }
    }
}

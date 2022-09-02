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
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private DataBaseContext data_context;
        public AuthenticateController(IConfiguration configuration, DataBaseContext data_context)
        {
            Configuration = configuration;
            this.data_context = data_context;
        }

        public IConfiguration Configuration { get; }

        public IActionResult Post()
        {

            var authorizationHeader = Request.Headers["Authorization"].First();
            var key = authorizationHeader.Split(' ')[1];
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(':');
            var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));

            Users user = this.data_context.User.Where(u => u.Email == credentials[0] && u.Password == credentials[1]).FirstOrDefault();

            if (user != null)
            {
                var result = new
                {
                    token = GenerateToken(serverSecret, user)
                };
                return Ok(result);//status code
            }
            return BadRequest("Invalid Email/Password");//status code
        }

        private string GenerateToken(SecurityKey key, Users user)
        {
            var now = DateTime.UtcNow;
            var issuer = Configuration["JWT:Issuer"];
            var audience = Configuration["JWT:Audience"];
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                });
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(issuer, audience, identity,
            now, now.Add(TimeSpan.FromHours(1)), now, signingCredentials);
            var encodedJwt = handler.WriteToken(token);
            return encodedJwt;
        }

    }


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
            return this.user_data_context.User.Where(user => user.Id == id).FirstOrDefault();
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
        // [Authorize(Roles = Roles.Admin)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users New_User)
        {
            this.user_data_context.User.Update(New_User);
            this.user_data_context.SaveChanges();
        }

        // DELETE
        // [Authorize(Roles = Roles.Admin)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.user_data_context.User.Remove(this.user_data_context.User.Where(New_User => New_User.Id == id).FirstOrDefault());
            this.user_data_context.SaveChanges();
        }

    }
}

using System;
namespace BackendAPI.Models
{
    public class Users
    {
        public Users()
        {
        }
        public int Id { get; set; }

        // BASIC DATA FIELD
        public string FirstName { get; set; }
        public string LastName { get; set; }


        // AUTHENTICATION DATA FIELD
        public string Email { get; set; }
        public string Password { get; set; }


        // ROLE DATA FIELD
        public string Role { get; set; } = "Customer";


        // DATA REGISTRATION FIELD
        public int MobileNumber { get; set; }
        public int AadharCard { get; set; }
        public string PAN_Card { get; set; }
        public string Address { get; set; }
        public string Nominee { get; set; }
    }
}

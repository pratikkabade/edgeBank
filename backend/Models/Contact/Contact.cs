using System;
using System.Text.Json.Serialization;

namespace BackendAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}

using System;
namespace frontend.Models
{
    public class Contact
    {
        public Contact()
        {
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}

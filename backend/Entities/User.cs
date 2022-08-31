namespace BackendAPI.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Registration Data
    public int MobileNumber { get; set; }
    public int AadharCard { get; set; }
    public string PAN_Card { get; set; }
    public string Address { get; set; }
    public string Nominee { get; set; }

    // Credentials
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public string Role { get; set; }
}
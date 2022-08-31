namespace BackendAPI.Models.Users;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public int MobileNumber { get; set; }

    [Required]
    public int AadharCard { get; set; }

    [Required]
    public string PAN_Card { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Nominee { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    // Customer by default
    public string Role { get; set; } = Roles.Customer;
}
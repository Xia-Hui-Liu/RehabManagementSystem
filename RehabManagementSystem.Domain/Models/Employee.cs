using Microsoft.AspNetCore.Identity;

namespace RehabManagementSystem.Domain;
public class Employee : IdentityUser 
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
   
}
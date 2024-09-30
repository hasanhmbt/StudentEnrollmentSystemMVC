using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentSystemMVC.Models;

public class ApplicationUser : IdentityUser
{
    [Display(Name = "Profile Picture")]
    public string? ProfilePicturePath { get; set; }
}

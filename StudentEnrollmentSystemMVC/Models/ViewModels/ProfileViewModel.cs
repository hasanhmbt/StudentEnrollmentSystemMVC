using System.ComponentModel.DataAnnotations;
namespace StudentEnrollmentSystemMVC.Models.ViewModels;

public class ProfileViewModel
{
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Profile Picture")]
    public string? ProfilePicturePath { get; set; }
}


using System.ComponentModel.DataAnnotations;
namespace StudentEnrollmentSystemMVC.Models.ViewModels;

public class EditProfileViewModel
{
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Profile Picture")]
    public IFormFile? ProfilePicture { get; set; }

    public string? ExistingProfilePicturePath { get; set; }
}
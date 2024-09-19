using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentSystemMVC.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public List<string> UserRoles { get; set; }
    }
}

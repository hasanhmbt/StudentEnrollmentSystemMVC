using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;

namespace StudentEnrollmentSystemMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppIdentityDbContext _identityContext;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityContext = context;
        }

        public async Task<IActionResult> CreateRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            return View(Tuple.Create(users, roles));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoles(string userId, string roleId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var role = await _roleManager.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
            if (user != null && role != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role assigned successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error assigning role!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "User or role not found!";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;

namespace StudentEnrollmentSystemMVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Enrollments/Enroll/5
        public async Task<IActionResult> Enroll(int id)
        {
            ViewBag.Students = await _context.Students.ToListAsync();
            var enrollment = await GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound($"Enrollment with ID {id} not found.");
            }

            SetGradeViewBag();
            return View(enrollment);
        }

        // POST: Enrollments/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int id, Grade grade)
        {
            if (!Enum.IsDefined(typeof(Grade), grade))
            {
                ModelState.AddModelError(string.Empty, "Invalid grade.");
                SetGradeViewBag();
                return View(await GetEnrollmentByIdAsync(id));
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound($"Enrollment with ID {id} not found.");
            }

            try
            {
                enrollment.Grade = grade;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Courses");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Please try again.");
                SetGradeViewBag();
                return View(enrollment);
            }
        }

        private async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        private void SetGradeViewBag()
        {
            ViewBag.Grades = Enum.GetValues(typeof(Grade));
        }
    }
}

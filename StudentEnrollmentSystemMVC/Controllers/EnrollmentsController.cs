using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;

namespace StudentEnrollmentSystemMVC.Controllers;

public class EnrollmentsController : Controller
{
    private readonly SchoolContext _context;

    public EnrollmentsController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Edit(int id)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrollment == null) return NotFound();

        ViewBag.Grades = Enum.GetValues(typeof(Grade));
        return View(enrollment);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Grade grade)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        enrollment.Grade = grade;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}

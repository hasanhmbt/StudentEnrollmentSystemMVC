using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;
namespace StudentEnrollmentSystemMVC.Controllers;

[Authorize(Roles = "Admin, Teacher")]
public class CourseController : Controller
{
    private readonly SchoolContext _context;

    public CourseController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Course
    public async Task<IActionResult> Index()
    {

        var courses = await _context.Courses.Include(c => c.Enrollments).ThenInclude(c => c.Student).ToListAsync();
        return View(courses);
    }

    // GET: Course/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(c => c.Student)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    // GET: Course/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Course/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Credits")] Course course)
    {
        if (ModelState.IsValid)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Course/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = _context.Courses.Find(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // POST: Course/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Title,Credits")] Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }



    // POST: Course/Delete/5
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.Find(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
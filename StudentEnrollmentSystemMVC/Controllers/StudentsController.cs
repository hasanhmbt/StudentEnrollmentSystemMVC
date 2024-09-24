using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;

namespace StudentEnrollmentSystemMVC.Controllers;
[Authorize(Roles = ("Admin,Teacher"))]
public class StudentsController : Controller
{
    private readonly SchoolContext _context;

    private readonly int PageSize = 10;
    public StudentsController(SchoolContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1)
    {
        var studentsQuery = _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            studentsQuery = studentsQuery.Where(s => s.Name.Contains(searchQuery) || s.Email.Contains(searchQuery));
        }

        var totalStudents = await studentsQuery.CountAsync();
        var students = await studentsQuery
            .OrderBy(s => s.Name)
            .Skip((pageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.CurrentSearch = searchQuery;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = (int)Math.Ceiling(totalStudents / (double)PageSize);

        return View(students);
    }




    public async Task<IActionResult> Details(int id)
    {
        var student = await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();
        return View(student);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return BadRequest();
        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }




}

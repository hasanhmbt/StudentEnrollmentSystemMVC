// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;
using System.Diagnostics;

namespace StudentEnrollmentSystemMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Data for Bar Chart
            var courseData = _context.Courses
                .Select(c => new
                {
                    c.Title,
                    StudentCount = c.Enrollments.Count()
                })
                .ToList();

            ViewBag.ChartLabels = string.Join(",", courseData.Select(c => $"\"{c.Title}\""));
            ViewBag.ChartData = string.Join(",", courseData.Select(c => c.StudentCount));




            // Data for Pie Chart (Grade Distribution)
            var gradeData = _context.Enrollments
                .GroupBy(e => e.Grade)
                .Select(g => new
                {
                    Grade = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();

            ViewBag.PieLabels = string.Join(",", gradeData.Select(g => $"\"{g.Grade}\""));
            ViewBag.PieData = string.Join(",", gradeData.Select(g => g.Count));





            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

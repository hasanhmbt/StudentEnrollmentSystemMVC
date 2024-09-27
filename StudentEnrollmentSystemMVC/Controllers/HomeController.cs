// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystemMVC.Data;
using StudentEnrollmentSystemMVC.Models;
using StudentEnrollmentSystemMVC.Models.ViewModels;
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
            int totalStudents = _context.Students.Count();
            int totalCourses = _context.Courses.Count();

            var dashboardCards = new List<DashboardCardViewModel>
            {
                new DashboardCardViewModel
                {
                    Title = "Total Students",
                    Value = totalStudents.ToString(),
                    IconClass = "fas fa-user-graduate",
                    BorderColor = "danger",
                    ValueColor = "text-gray-800"
                },
                new DashboardCardViewModel
                {
                    Title = "Total Courses",
                    Value = totalCourses.ToString(),
                    IconClass = "fas fa-calendar",
                    BorderColor = "primary",
                    ValueColor = "text-gray-800"
                },
                new DashboardCardViewModel
                {
                    Title = "Earnings (Annual)",
                    Value = "$215,000",
                    IconClass = "fas fa-dollar-sign",
                    BorderColor = "success",
                    ValueColor = "text-gray-800"
                },
                new DashboardCardViewModel
                {
                    Title = "Tasks",
                    Value = "50%",
                    AdditionalInfo = "50",
                    ProgressPercentage = "50",
                    IconClass = "fas fa-clipboard-list",
                    BorderColor = "info",
                    ValueColor = "text-gray-800"
                }

            };



            ViewData["DashboardCards"] = dashboardCards;







            #region Charts

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
            #endregion



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

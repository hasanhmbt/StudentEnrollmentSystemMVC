using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystemMVC.Models.ViewModels;

namespace StudentEnrollmentSystemMVC.ViewComponents;

public class DashboardCards : ViewComponent
{
    public IViewComponentResult Invoke(List<DashboardCardViewModel> cards)
    {
        return View(cards);
    }
}
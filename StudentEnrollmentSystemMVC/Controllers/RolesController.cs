using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Models.ViewModels;

namespace StudentEnrollmentSystemMVC.Controllers;

public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var rolesViewModel = new List<RolesViewModel>();

        foreach (var role in roles)
        {
            var thisViewModel = new RolesViewModel
            {
                Id = role.Id,
                Name = role.Name

            };
            rolesViewModel.Add(thisViewModel);
        }

        return View(rolesViewModel);
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RolesViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = new IdentityRole
            {
                Name = model.Name
            };
            await _roleManager.CreateAsync(role);
            return RedirectToAction(nameof(Index));

        }
        return View(model);

    }



    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        var model = new RolesViewModel
        {
            Id = role.Id,
            Name = role.Name
        };
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RolesViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            await _roleManager.UpdateAsync(role);
            return RedirectToAction(nameof(Index));

        }
        return View(model);

    }


    public async Task<IActionResult> Delete(string Id)
    {


        if (ModelState.IsValid)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction(nameof(Index));

    }










}

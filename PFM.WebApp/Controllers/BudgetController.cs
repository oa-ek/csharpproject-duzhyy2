using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories.Budgets;
using PFM.Persistence.Repositories.Categories;
using PFM.WebApp.Dtos;

namespace PFM.WebApp.Controllers;

public class BudgetController(
    IBudgetRepository budgetRepository,
    ICategoryRepository categoryRepository,
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var currentUser = await userManager.GetUserAsync(User);
        return View(await budgetRepository.GetAllByUserAsync(currentUser));
    }
    
    public async Task<IActionResult> Create()
    {
        var currentUser = await userManager.GetUserAsync(User);
        ViewBag.Categories = (await categoryRepository.GetAllByUserAsync(currentUser))
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BudgetCreateDto model)
    {
        var entity = mapper.Map<Budget>(model);
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    entity.AppUser = user;
                    await budgetRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }
        }

        return View(entity);
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var currentUser = await userManager.GetUserAsync(User);
        ViewBag.Categories = (await categoryRepository.GetAllByUserAsync(currentUser))
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        var entity = await budgetRepository.GetAsync(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BudgetUpdateDto model, IFormCollection collection)
    {
        var entity = mapper.Map<Budget>(model);

        await budgetRepository.UpdateAsync(entity);
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        return View(id);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
    {
        await budgetRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}
using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories.Categories;
using PFM.Persistence.Repositories.TransactionTypes;
using PFM.WebApp.Dtos;

namespace PFM.WebApp.Controllers;

public class CategoryController(
    ICategoryRepository categoryRepository,
    ITransactionTypeRepository transactionTypeRepository,
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var currentUser = await userManager.GetUserAsync(User);
        return View(await categoryRepository.GetAllByUserAsync(currentUser));
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Types = (await transactionTypeRepository.GetAllAsync())
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDto model)
    {
        var entity = mapper.Map<Category>(model);
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    entity.AppUser = user;
                    await categoryRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }
        }

        return View(entity);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var entity = await categoryRepository.GetAsync(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryUpdateDto model, IFormCollection collection)
    {
        var entity = mapper.Map<Category>(model);

        await categoryRepository.UpdateAsync(entity);
        
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
        await categoryRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}
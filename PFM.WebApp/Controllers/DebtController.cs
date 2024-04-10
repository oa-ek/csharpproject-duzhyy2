using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories.Debts;
using PFM.WebApp.Dtos;

namespace PFM.WebApp.Controllers;

public class DebtController(
    IDebtRepository debtRepository,
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var currentUser = await userManager.GetUserAsync(User);
        return View(await debtRepository.GetAllByUserAsync(currentUser));
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DebtCreateDto model)
    {
        var entity = mapper.Map<Debt>(model);
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    entity.AppUser = user;
                    await debtRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }
        }

        return View(entity);
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var entity = await debtRepository.GetAsync(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DebtUpdateDto model, IFormCollection collection)
    {
        var entity = mapper.Map<Debt>(model);
        
        await debtRepository.UpdateAsync(entity);
        
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
        await debtRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}
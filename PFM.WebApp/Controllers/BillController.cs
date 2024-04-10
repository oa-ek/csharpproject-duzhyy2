using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories.Bills;
using PFM.Persistence.Repositories.BillTypes;
using PFM.WebApp.Dtos;

namespace PFM.WebApp.Controllers;

public class BillController(
    IBillRepository billRepository,
    IBillTypeRepository typeRepository,
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var currentUser = await userManager.GetUserAsync(User);
        return View(await billRepository.GetAllByUserAsync(currentUser));
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.BillTypes = (await typeRepository.GetAllAsync())
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BillCreateDto model)
    {
        var entity = mapper.Map<Bill>(model);
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    entity.AppUser = user;
                    await billRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }
        }

        return View(entity);
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        ViewBag.Types = (await typeRepository.GetAllAsync())
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        var entity = await billRepository.GetAsync(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BillUpdateDto model, IFormCollection collection)
    {
        var entity = mapper.Map<Bill>(model);

        await billRepository.UpdateAsync(entity);
        
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
        await billRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}
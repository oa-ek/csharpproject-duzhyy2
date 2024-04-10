using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories.Bills;
using PFM.Persistence.Repositories.Categories;
using PFM.Persistence.Repositories.Transactions;
using PFM.Persistence.Repositories.TransactionTypes;
using PFM.WebApp.Dtos;

namespace PFM.WebApp.Controllers;

public class TransactionController(
    ITransactionRepository transactionRepository,
    ITransactionTypeRepository transactionTypeRepository,
    IBillRepository billRepository,
    ICategoryRepository categoryRepository,
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var currentUser = await userManager.GetUserAsync(User);
        return View(await transactionRepository.GetAllByUserAsync(currentUser));
    }
    
    public async Task<IActionResult> Create()
    {
        var currentUser = await userManager.GetUserAsync(User);
        
        ViewBag.Types = (await transactionTypeRepository.GetAllByUserAsync(currentUser))
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        ViewBag.Bills = (await billRepository.GetAllByUserAsync(currentUser))
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        ViewBag.Categories = (await categoryRepository.GetAllByUserAsync(currentUser))
            .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionCreateDto model)
    {
        var entity = mapper.Map<Transaction>(model);
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    entity.AppUser = user;
                    await transactionRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }
        }

        return View(entity);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var entity = await transactionRepository.GetAsync(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TransactionUpdateDto model, IFormCollection collection)
    {
        var entity = mapper.Map<Transaction>(model);

        await transactionRepository.UpdateAsync(entity);
        
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
        await transactionRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}
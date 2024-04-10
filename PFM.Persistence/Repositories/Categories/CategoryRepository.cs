using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Categories;

public class CategoryRepository(AppDbContext context) : Repository<Category, Guid>(context), ICategoryRepository
{
    public override async Task<IEnumerable<Category>> GetAllByUserAsync(AppUser user) =>
        await context.Categories.Where(x => x.AppUser == user).ToListAsync();
}
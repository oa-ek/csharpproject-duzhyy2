using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Budgets;

public class BudgetRepository(AppDbContext context) : Repository<Budget, Guid>(context), IBudgetRepository
{
    public override async Task<IEnumerable<Budget>> GetAllByUserAsync(AppUser user) =>
        await context.Budgets.Where(x => x.AppUser == user).ToListAsync();
}
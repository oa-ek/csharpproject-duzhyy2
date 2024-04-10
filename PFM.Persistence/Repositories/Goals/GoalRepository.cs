using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Goals;

public class GoalRepository(AppDbContext context) : Repository<Goal, Guid>(context), IGoalRepository
{
    public override async Task<IEnumerable<Goal>> GetAllByUserAsync(AppUser user) =>
        await context.Goals.Where(x => x.AppUser == user).ToListAsync();
}
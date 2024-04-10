using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Debts;

public class DebtRepository(AppDbContext context) : Repository<Debt, Guid>(context), IDebtRepository
{
    public override async Task<IEnumerable<Debt>> GetAllByUserAsync(AppUser user) =>
        await context.Debts.Where(x => x.AppUser == user).ToListAsync();
}
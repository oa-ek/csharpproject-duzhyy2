using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Bills;

public class BillRepository(AppDbContext context) : Repository<Bill, Guid>(context), IBillRepository
{
    public override async Task<IEnumerable<Bill>> GetAllByUserAsync(AppUser user) =>
        await context.Bills.Where(x => x.AppUser == user).ToListAsync();
}
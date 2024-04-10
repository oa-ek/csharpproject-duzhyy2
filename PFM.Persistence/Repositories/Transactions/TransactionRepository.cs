using Microsoft.EntityFrameworkCore;

using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Transactions;

public class TransactionRepository(AppDbContext context)
    : Repository<Transaction, Guid>(context), ITransactionRepository
{
    public override async Task<IEnumerable<Transaction>> GetAllByUserAsync(AppUser user) =>
        await context.Transactions.Where(x => x.AppUser == user).ToListAsync();
}
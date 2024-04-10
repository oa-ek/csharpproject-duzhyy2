using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.TransactionTypes;

public class TransactionTypeRepository(AppDbContext context)
    : Repository<TransactionType, Guid>(context), ITransactionTypeRepository { }
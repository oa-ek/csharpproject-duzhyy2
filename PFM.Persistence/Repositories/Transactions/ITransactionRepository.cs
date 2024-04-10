using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.Transactions;

public interface ITransactionRepository : IRepository<Transaction, Guid> { }
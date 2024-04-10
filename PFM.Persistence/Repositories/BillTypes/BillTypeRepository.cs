using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories.BillTypes;

public class BillTypeRepository(AppDbContext context) : Repository<BillType, Guid>(context), IBillTypeRepository;
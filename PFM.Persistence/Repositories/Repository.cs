using Microsoft.EntityFrameworkCore;

using PFM.Domain;
using PFM.Domain.Entities;

namespace PFM.Persistence.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
{
    protected AppDbContext _context;
    //protected DbSet<TEntity> dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        //dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

    public virtual async Task<IEnumerable<TEntity>> GetAllByUserAsync(AppUser user) =>
        await _context.Set<TEntity>().ToListAsync();

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await SaveAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await SaveAsync();
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        _context.Set<TEntity>().Remove(await _context.Set<TEntity>().FindAsync(id));
        await SaveAsync();
    }

    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
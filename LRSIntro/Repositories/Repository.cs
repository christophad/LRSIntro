using LRSIntro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public Repository(LRSIntroContext lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _lRSIntroContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await _lRSIntroContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _lRSIntroContext.Set<TEntity>().Remove(entity);
            _lRSIntroContext.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _lRSIntroContext.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _lRSIntroContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _lRSIntroContext.Update(entity);
            await _lRSIntroContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
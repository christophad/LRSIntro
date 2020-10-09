using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    /// <summary>
    /// Contains common methods for all entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TEntity}"/>.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets an entity by it's Id.
        /// </summary>
        /// <param name="id">The integer id of the entity.</param>
        /// <returns>A <see cref="TEntity"/>.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Add method
        /// Receives an entity and adds it to the Repository
        /// </summary>
        /// <param name="entity">An instance of type TEntity</param>
        /// <returns>The added <paramref name="entity"/>.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Remove method
        /// Receives an entity and removes it from the Repository
        /// </summary>
        /// <param name="entity">An instance of type TEntity</param>
        void Delete(TEntity entity);
    }
}

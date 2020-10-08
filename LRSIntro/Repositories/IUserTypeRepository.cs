using LRSIntro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public interface IUserTypeRepository : IRepository<UserType>
    {
        /// <summary>
        /// Returns a list of all user types
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserType}"/></returns>
        Task<IEnumerable<UserType>> GetAllUserTypesAsync();
    }
}

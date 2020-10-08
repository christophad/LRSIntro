using LRSIntro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public interface IUserTitleRepository : IRepository<UserTitle>
    {
        /// <summary>
        /// Returns a list of all user titles
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserTitle}"/></returns>
        Task<IEnumerable<UserTitle>> GetAllUserTitlesAsync();
    }
}

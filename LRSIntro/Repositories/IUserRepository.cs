using LRSIntro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Returns a list of all active users
        /// </summary>
        /// <returns>A <see cref="IEnumerable{User}"/></returns>
        Task<IEnumerable<User>> GetAllUsersWithDetailsAsync();

        /// <summary>
        /// Returns a User by user identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>A <see cref="User"/></returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// LogicalLy deletes a user.
        /// </summary>
        /// <param name="user">The user</param>
        void DeleteUser(User user);

        /// <summary>
        /// Search users
        /// </summary>
        /// <returns>A <see cref="IEnumerable{User}"/></returns>
        Task<IEnumerable<User>> SearchUsers(string searchTerm);
    }
}
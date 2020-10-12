using LRSIntro.DTO;
using LRSIntro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Returns a list of all active users
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserDTO}"/></returns>
        Task<IEnumerable<UserDTO>> GetAllUsersWithDetailsAsync();

        /// <summary>
        /// Returns a User by user identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>A <see cref="UserDTO"/></returns>
        Task<UserDTO> GetUserByIdAsync(int id);

        /// <summary>
        /// Returns a list of all user titles
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserTitleDTO}"/></returns>
        Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync();

        /// <summary>
        /// Returns a list of all user types
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserTypeDTO}"/></returns>
        Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync();

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="userAddOrUpdateDTO">The request data</param>
        /// <returns>A <see cref="UserDTO"/></returns>
        Task<UserDTO> UpdateUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO);

        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="userAddOrUpdateDTO">The request data</param>
        /// <returns>A <see cref="UserDTO"/></returns>
        Task<UserDTO> AddUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO);

        /// <summary>
        /// Logically deletes a user
        /// </summary>
        /// <param name="id">The user identifier</param>
        Task DeleteUserAsync(int id);

        /// <summary>
        /// Search users
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserDTO}"/></returns>
        Task<IEnumerable<UserDTO>> SearchUsersAsync(string searchTerm);
    }
}

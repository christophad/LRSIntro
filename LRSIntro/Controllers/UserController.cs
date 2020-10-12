using LRSIntro.DTO;
using LRSIntro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(logger));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a list of all active Users
        /// </summary>
        /// <returns cref="IEnumerable{UserDTO}"></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsersWithDetailsAsync().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets a User by identifier
        /// </summary>
        /// <param name="id">The user identifier</param>
        /// <returns cref="UserDTO"></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(id).ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Returns a list of all user titles
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserTitleDTO}"/></returns>
        [HttpGet]
        [Route("Titles")]
        public async Task<ActionResult<IEnumerable<UserTitleDTO>>> GetUserTitles()
        {
            try
            {
                return Ok(await _userService.GetUserTitlesAsync().ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all user types
        /// </summary>
        /// <returns>A <see cref="IEnumerable{UserTypeDTO}"/></returns>
        [HttpGet]
        [Route("Types")]
        public async Task<ActionResult<IEnumerable<UserTypeDTO>>> GetUserTypes()
        {
            try
            {
                return Ok(await _userService.GetUserTypesAsync().ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="userAddOrUpdateDTO">The request data</param>
        /// <returns>A <see cref="UserDTO"/></returns>
        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            try
            {
                return Ok(await _userService.UpdateUserAsync(userAddOrUpdateDTO).ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="userAddOrUpdateDTO">The request data</param>
        /// <returns>A <see cref="UserDTO"/></returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            try
            {
                return Ok(await _userService.AddUserAsync(userAddOrUpdateDTO).ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">The user identifier</param>
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <returns cref="IEnumerable{UserDTO}"></returns>
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> SearchUsers(string searchTerm)
        {
            try
            {
                return Ok(await _userService.SearchUsersAsync(searchTerm).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

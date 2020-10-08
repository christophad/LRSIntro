using AutoMapper;
using AutoMapper.QueryableExtensions;
using LRSIntro.DTO;
using LRSIntro.Models;
using LRSIntro.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSIntro.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserTitleRepository _userTitleRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly ILogger<IUserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserTypeRepository userTypeRepository, IUserTitleRepository userTitleRepository, ILogger<IUserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
            // TODO argument checks
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersWithDetailsAsync()
        {
            var res = await _userRepository.GetAllUsersWithDetailsAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserDTO>>(res);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if (id <= default(int))
            {
                _logger?.LogInformation("Get user by Id failed, User identifier is required.");
                throw new ArgumentException("User id is required.", paramName: nameof(id));
            }

            // TODO not needed try catch since you already handle it on controller.
            try
            {
                var res = await _userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
                return _mapper.Map<UserDTO>(res);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UserEditDTO> GetUserEditByIdAsync(int id)
        {
            if (id <= default(int))
            {
                _logger?.LogInformation("Get user by Id failed, User identifier is required.");
                throw new ArgumentException("User id is required.", paramName: nameof(id));
            }

            try
            {
                var res = await _userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
                return _mapper.Map<UserEditDTO>(res);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync()
        {
            try
            {
                var res = await _userTitleRepository.GetAllUserTitlesAsync().ConfigureAwait(false);
                return _mapper.Map<IEnumerable<UserTitleDTO>>(res);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync()
        {
            try
            {
                var res = await _userTypeRepository.GetAllUserTypesAsync().ConfigureAwait(false);
                return _mapper.Map<IEnumerable<UserTypeDTO>>(res);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UserDTO> UpdateUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            if (userAddOrUpdateDTO == null)
            {
                // TODO not information this is an error
                _logger?.LogInformation("Update failed. Update data is missing");
                throw new ArgumentException("Update data is missing.", paramName: nameof(userAddOrUpdateDTO));
            }

            if (userAddOrUpdateDTO.Id == null)
            {
                _logger?.LogInformation("Update failed, User identifier is required.");
                throw new ArgumentException("User id is required.", paramName: nameof(userAddOrUpdateDTO.Id));
            }

            try
            {
                var res = await _userRepository.UpdateAsync(_mapper.Map<User>(userAddOrUpdateDTO)).ConfigureAwait(false);
                // TODO what if user does not exist? throw bad request not 500
                var user = await _userRepository.GetUserByIdAsync(res.Id).ConfigureAwait(false);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UserDTO> AddUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            if (userAddOrUpdateDTO == null)
            {
                _logger?.LogInformation("Add user failed. Update data is missing");
                throw new ArgumentException("user data is missing.", paramName: nameof(userAddOrUpdateDTO));
            }

            try
            {
                var res = await _userRepository.AddAsync(_mapper.Map<User>(userAddOrUpdateDTO)).ConfigureAwait(false);
                var user = await _userRepository.GetUserByIdAsync(res.Id).ConfigureAwait(false);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= default(int))
            {
                _logger?.LogInformation("Delete user failed, User identifier is required.");
                throw new ArgumentException("User id is required.", paramName: nameof(id));
            }

            try
            {
                var user = await _userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
                // TODO what if user does not exist? throw bad request not 500
                _userRepository.Delete(user);
            }
            catch (Exception ex)
            {
                // TODO why handle exceptions here???
                _logger?.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

using AutoMapper;
using LRSIntro.DTO;
using LRSIntro.Models;
using LRSIntro.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserTitleRepository _userTitleRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserTypeRepository userTypeRepository, IUserTitleRepository userTitleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userTitleRepository = userTitleRepository ?? throw new ArgumentNullException(nameof(userTitleRepository));
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException(nameof(userTypeRepository));
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
                throw new ArgumentException("User id is required.", paramName: nameof(id));
            }

            var res = await _userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<UserDTO>(res);
        }

        public async Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync()
        {
            var res = await _userTitleRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserTitleDTO>>(res);
        }

        public async Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync()
        {
            var res = await _userTypeRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserTypeDTO>>(res);
        }

        public async Task<UserDTO> UpdateUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            if (userAddOrUpdateDTO == null)
            {
                throw new ArgumentException("Update data is missing.", paramName: nameof(userAddOrUpdateDTO));
            }

            if (userAddOrUpdateDTO.Id == null)
            {
                throw new ArgumentException("User id is required.", paramName: nameof(userAddOrUpdateDTO.Id));
            }

            var res = await _userRepository.UpdateAsync(_mapper.Map<User>(userAddOrUpdateDTO)).ConfigureAwait(false);
            if (res == null)
            {
                throw new ArgumentException($"User with id {userAddOrUpdateDTO.Id} was not found in the database");
            }
            var user = await _userRepository.GetUserByIdAsync(res.Id).ConfigureAwait(false);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddUserAsync(UserAddOrUpdateDTO userAddOrUpdateDTO)
        {
            if (userAddOrUpdateDTO == null)
            {
                throw new ArgumentException("user data is missing.", paramName: nameof(userAddOrUpdateDTO));
            }

            var res = await _userRepository.AddAsync(_mapper.Map<User>(userAddOrUpdateDTO)).ConfigureAwait(false);

            var user = await _userRepository.GetUserByIdAsync(res.Id).ConfigureAwait(false);
            return _mapper.Map<UserDTO>(user);

        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= default(int))
            {
                throw new ArgumentException("User id is required.", paramName: nameof(id));
            }

            var user = await _userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} was not found in the database");
            }
            _userRepository.DeleteUser(user);
        }

        public async Task<IEnumerable<UserDTO>> SearchUsersAsync(string searchTerm)
        {
            var res = await _userRepository.SearchUsers(searchTerm).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserDTO>>(res);
        }
    }
}

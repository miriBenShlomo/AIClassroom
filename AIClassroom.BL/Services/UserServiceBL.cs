using AIClassroom.BL.API;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using AIClassroom.BL.ModelsDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIClassroom.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new ArgumentException("User name cannot be empty.");

            var user = _mapper.Map<User>(userDto);
            var newUserFromDb = await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserDto>(newUserFromDb);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid User ID.");

            var user = await _userRepository.GetUserByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            if (userDto.Id <= 0)
                throw new ArgumentException("Invalid User ID.");

            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new ArgumentException("User name cannot be empty.");

            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid User ID.");

            await _userRepository.DeleteUserAsync(id);
        }
    }
}
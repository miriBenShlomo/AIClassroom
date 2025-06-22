using AIClassroom.BL.ModelsDTO;
using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.BL.API
{
    public interface IUserService
    {
        // מקורי
        // מתוקן
        Task<UserDto> AddUserAsync(UserDto userDto);
        Task DeleteUserAsync(int id);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UserDto userDto);
    }
}

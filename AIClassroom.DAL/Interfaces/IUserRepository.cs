using AIClassroom.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIClassroom.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);

        Task<User?> GetUserByNameAndPhoneAsync(string name, string phone);
    }
}

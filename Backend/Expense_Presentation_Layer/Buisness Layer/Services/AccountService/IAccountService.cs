using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Services.AccountService
{
    public interface IAccountService
    {
        Task<UserDTO?> LoginAsync(string email, string password);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task UpdateAsync(UserModel user, int userId);
    }
}

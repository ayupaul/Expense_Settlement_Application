using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.IRepo
{
    public interface IAccount
    {
        Task<UserModel?> VerifyUserAsync(string Email,string Password);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task UpdateAmountAsync(UserModel user, int Amount);
        Task<UserModel> GetUserByIdAsync(int id);
        Task settleAmountAsync(int amount,int userId);
        Task UpdateUserAsync(UserModel user, int userId);
    }
}

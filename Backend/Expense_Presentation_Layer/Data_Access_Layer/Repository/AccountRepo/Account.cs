using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.IRepo
{
    public class Account : IAccount
    {
        private readonly AppDbContext appDbContext;

        public Account(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users=await appDbContext.Users.ToListAsync();
            return users;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user=await appDbContext.Users.FirstOrDefaultAsync(x=> x.Email == email);
            return user;
        }

        public async Task<UserModel?> VerifyUserAsync(string Email, string Password)
        {

            var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
            return user;

        }
        public async Task UpdateAmountAsync(UserModel user,int Amount)
        {
            user.Amount -= Amount;
            await appDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user=await appDbContext.Users.FirstOrDefaultAsync(x=>x.Id == id);
            return user;
        }

        public async Task settleAmountAsync(int amount,int userId)
        {
            var user = await GetUserByIdAsync(userId);
            user.Amount += amount;
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserModel user,int userId)
        {
           var userDetail=await appDbContext.Users.FirstOrDefaultAsync(x=>x.Id==userId);
            if (userDetail != null)
            {
               userDetail.UserName=user.UserName;
                userDetail.Email = user.Email;
                userDetail.Amount = user.Amount;
            }
           await appDbContext.SaveChangesAsync();
        }
    }
}

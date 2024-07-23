using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.GroupRepo
{
    public class GroupRepo : IGroupRepo
    {
        private readonly AppDbContext appDbContext;

        public GroupRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddUserToGroupAsync(int userId, int groupId)
        {
            var userGroup=new UserGroupModel() { GroupId=groupId,UserId=userId};
            await appDbContext.AddAsync(userGroup);
            await appDbContext.SaveChangesAsync();
            
        }

        public async Task<GroupModel> CreateGroupAsync(GroupModel groupModel,int userId)
        {
            await appDbContext.Groups.AddAsync(groupModel);
            await appDbContext.SaveChangesAsync();
            var groupId=groupModel.GroupId;
            var userGroup=new UserGroupModel() { GroupId = groupId ,UserId=userId};
            await appDbContext.AddAsync(userGroup);
            await appDbContext.SaveChangesAsync();
            return groupModel;
        }

        public async Task<List<UserGroupModel>> GetAllUserInGroupById(int groupId)
        {
            var groups=await appDbContext.UserGroups.Where(x=>x.GroupId==groupId).ToListAsync(); ;
            return groups;
        }

        public async Task<List<UserGroupModel>> GetMyGroupsAsync(int userId)
        {
            var groups = await appDbContext.UserGroups.Include(x => x.Group).Where(u => u.UserId == userId).ToListAsync();
            return groups;
        }

        public async Task<List<UserGroupModel>> GetUserInGroupAsync(int groupId)
        {
            var users=await appDbContext.UserGroups.Include(x=>x.User).Where(x=>x.GroupId==groupId).ToListAsync();
            return users;
        }
    }
}

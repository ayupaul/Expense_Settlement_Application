using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.GroupRepo
{
    public interface IGroupRepo
    {
        Task<GroupModel> CreateGroupAsync(GroupModel model,int userId);
        Task<List<UserGroupModel>> GetMyGroupsAsync(int userId);
        Task<List<UserGroupModel>> GetUserInGroupAsync(int groupId);
        Task AddUserToGroupAsync(int userId,int groupId);
        Task<List<UserGroupModel>> GetAllUserInGroupById(int groupId);
    }
}

using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Services.GroupService
{
    public interface IGroupService
    {
        Task<GroupDTO> CreateGroupAsync(GroupModel group,int userId); 
        Task<List<GroupModel>> GetMyGroupsAsync(int userId);
        Task<List<UserModel>> GetUsersInGroupAsync(int groupId);
        Task<bool> AddUserToGroupAsync(int userId, int groupId);
    }
}

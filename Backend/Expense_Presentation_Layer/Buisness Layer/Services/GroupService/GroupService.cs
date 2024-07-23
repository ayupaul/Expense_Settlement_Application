using AutoMapper;
using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.GroupRepo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Services.GroupService
{
    
    public class GroupService : IGroupService
    {
        private readonly IGroupRepo groupRepo;
        private readonly IValidator<GroupModel> validator;
        private readonly IMapper mapper;

        public GroupService(IGroupRepo groupRepo,IValidator<GroupModel> validator,IMapper mapper)
        {
            this.groupRepo = groupRepo;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<bool> AddUserToGroupAsync(int userId, int groupId)
        {
            var usersInGroup = await groupRepo.GetUserInGroupAsync(groupId);
            if (usersInGroup.Count > 10)
            {
                return false;
            }
            await groupRepo.AddUserToGroupAsync(userId, groupId);
            return true;
        }

        public async Task<GroupDTO>? CreateGroupAsync(GroupModel group,int userId)
        {
            if(group == null)
            {
                return null;
            }
            try
            {
                var validationResult = await validator.ValidateAsync(group);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                var groupDetail = await groupRepo.CreateGroupAsync(group,userId);
                var groupDTODetail = mapper.Map<GroupDTO>(groupDetail);
                return groupDTODetail;
            }
            catch(ValidationException ex)
            {
                var errors=string.Join(", ", ex.Errors.Select(e=>e.ErrorMessage));
                await Console.Out.WriteLineAsync(errors);
                return new GroupDTO { Error= errors };
            }
        }

        public async Task<List<GroupModel>> GetMyGroupsAsync(int userId)
        {
            var groups = await groupRepo.GetMyGroupsAsync(userId);
            return groups.Select(x => x.Group).ToList();
        }

        public async Task<List<UserModel>> GetUsersInGroupAsync(int groupId)
        {
            var users=await groupRepo.GetUserInGroupAsync(groupId);
            return users.Select(x=> x.User).ToList();
        }
    }
}

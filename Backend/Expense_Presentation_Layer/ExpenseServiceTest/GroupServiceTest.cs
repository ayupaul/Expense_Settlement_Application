using AutoMapper;
using Buisness_Layer.AutoMapper;
using Buisness_Layer.DTOs;
using Buisness_Layer.Services.GroupService;
using Buisness_Layer.Validation;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.GroupRepo;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseServiceTest
{
    public class GroupServiceTest
    {
        private readonly Mock<IGroupRepo> _groupRepoMock;
        private readonly IValidator<GroupModel> _validator;
        private readonly IMapper _mapper;
        private readonly GroupService _groupService;
        public GroupServiceTest() { 
            _groupRepoMock = new Mock<IGroupRepo>();
            _validator = new GroupValidator();
            _mapper=MappingConfig.Configure();
            _groupService=new GroupService(_groupRepoMock.Object, _validator,_mapper);
        }
        [Theory]
        [InlineData("","Fun bond group","20-08-2023","Group Name cannot be empty")]
        [InlineData("", "", "20-08-2023", "Group Name cannot be empty, Description cannot be empty")]
        public async Task CreateGroupAsync_Validation_Check(string groupName,string description,string createDate,string errorMessage)
        {
            //arrange
            var groupData = new GroupModel() { GroupName = groupName, Description = description, CreatedDate=createDate};
            _groupRepoMock.Setup(x=>x.CreateGroupAsync(groupData, 1)).ReturnsAsync(groupData);
            var groupDTOData = _mapper.Map<GroupDTO>(groupData);
            //act
            var result=await _groupService.CreateGroupAsync(groupData,1);
            Assert.Equal(errorMessage,result.Error);
        }
    }
}

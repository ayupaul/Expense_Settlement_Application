using Buisness_Layer.DTOs;
using Buisness_Layer.Services.GroupService;
using Data_Access_Layer.Models;
using Expense_Presentation_Layer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseServiceTest.Controller
{
    public class GroupControllerTest
    {
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly GroupController _controller;
        public GroupControllerTest()
        {
            _mockGroupService = new Mock<IGroupService>();
            _controller=new GroupController(_mockGroupService.Object);
        }
        [Fact]
        public async Task CreateGroupAsync_ReturnsBadRequest()
        {
            //arrange
            var group = new GroupModel();
            group = null;
            int userId = 1;
            //act
            var result = await _controller.CreateGroupAsync(group, userId);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task CreateGroupAsync_ReturnsOK()
        {
            //arrange
            var group = new GroupModel() { GroupId = 1, GroupName = "test" };
            var groupDTO=new GroupDTO() { GroupId=group.GroupId, GroupName=group.GroupName };
            int userId = 1;
            _mockGroupService.Setup(x => x.CreateGroupAsync(group, userId)).ReturnsAsync(groupDTO);
            //act
            var result = await _controller.CreateGroupAsync(group, userId);
            //assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task AddUserToGroupAsync_ReturnsBadRequest()
        {
            //arrange
            int groupId = 0;
            int userId = 1;
            //act
            var result = await _controller.AddUserToGroupAsync(userId,groupId);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task AddUserToGroupAsync_ReturnsOK()
        {
            //arrange
            int userId = 1;
            int groupId = 1;
            _mockGroupService.Setup(x => x.AddUserToGroupAsync(userId,groupId)).ReturnsAsync(true);
            //act
            var result = await _controller.AddUserToGroupAsync(userId,groupId);
            //assert
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task AddUserToGroupAsync_ReturnsInternalServerError()
        {
            //arrange
            int userId = 1;
            int groupId = 1;
            _mockGroupService.Setup(x => x.AddUserToGroupAsync(userId, groupId)).ReturnsAsync(false);
            //act
            var result = await _controller.AddUserToGroupAsync(userId, groupId);
            //assert
            Assert.IsType<ObjectResult>(result);
        }

    }
}

using Buisness_Layer.DTOs;
using Buisness_Layer.Services.AccountService;
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
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly AccountController _accountController;
        public AccountControllerTest()
        {
            _mockAccountService = new Mock<IAccountService>();
            _accountController=new AccountController(_mockAccountService.Object);
        }
        [Fact]
        public async Task LoginAsync_ReturnsBadRequestOnNull()
        {
            //arrange
            var user = new UserModel() { Email = "", Password = "" };
            _mockAccountService.Setup(x => x.LoginAsync(user.Email,user.Password)).ReturnsAsync((UserDTO)null);
            //act
            var result=await _accountController.LoginAsync(user);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task LoginAsync_ReturnsOKResponse()
        {
            //arrange
            var user = new UserModel() { Email = "test@gmail.com", Password = "Test123@" };
            var userDTO=new UserDTO() { Email = user.Email, Password = user.Password };
            _mockAccountService.Setup(x => x.LoginAsync(user.Email, user.Password)).ReturnsAsync(userDTO);
            //act
            var result = await _accountController.LoginAsync(user);
            //assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

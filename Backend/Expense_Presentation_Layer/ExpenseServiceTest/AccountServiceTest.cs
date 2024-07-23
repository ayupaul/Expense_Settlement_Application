using AutoMapper;
using Buisness_Layer.AutoMapper;
using Buisness_Layer.DTOs;
using Buisness_Layer.Services.AccountService;
using Buisness_Layer.Validation;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.IRepo;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ExpenseServiceTest
{
    public class AccountServiceTest
    {
        private readonly Mock<IAccount> accountRepo;
        private readonly IMapper mapper;
        private readonly IValidator<UserModel> validator;
        private readonly AccountService accountService;
        public AccountServiceTest() {
            accountRepo = new Mock<IAccount>();
            mapper=MappingConfig.Configure();
            validator = new AccountValidator();
            accountService = new AccountService(accountRepo.Object, validator, mapper);
        }
        [Theory]
        [InlineData("","")]
        [InlineData("ayush@gmail.com","")]
        public async Task LoginAsync_EmptyEmailOrPassword_Returns_Null(string email,string password)
        {
            //arrange
            
            var userDetails=new UserModel() { Email = email, Password = password ,Role="Admin"};
            accountRepo.Setup(x => x.VerifyUserAsync(email, password)).ReturnsAsync(userDetails);
            //act 
            var result=await accountService.LoginAsync(email,password);
            //assert
            Assert.Equal("Email or Password cannot be null", result.Error);
           
        }
        [Theory]
        //Testing email format
        [InlineData("ayushgmail.com","Ayush123@", "Email Address is not in format")]
        //Testing password format
        [InlineData("ayush@gmail.com","sda", "Password minimum length should be 8")]
        //If both format wrong
        [InlineData("ad","sda", "Email Address is not in format, Password minimum length should be 8")]
        public async Task LoginAsync_ValidationTest(string email,string password,string errorMessage)
        {
            //arrange
          
            var userDetails = new UserModel() { Email = email, Password = password, Role = "Admin" };
            accountRepo.Setup(x => x.VerifyUserAsync(email, password)).ReturnsAsync(userDetails);
            //act
            var result = await accountService.LoginAsync(email, password);
            //assert
            Assert.Equal(errorMessage, result.Error);
        }
        [Theory]
        //if string type or reference type variable is provided with value null 
        [InlineData("ayush@gmail.com","Ayush123@",null)]
       
       public async Task LoginAsync_Jwt_Invalid_ReturnsNull(string email,string password,string role)
        {
            //arrange

            var userDetails = new UserModel() { Email = email, Password = password,Role=role };
            accountRepo.Setup(x => x.VerifyUserAsync(email, password)).ReturnsAsync(userDetails);
            //act
            var result = await accountService.LoginAsync(email, password);
            //assert
            Assert.Equal("Something went wrong during creating of token", result.Error);
        }
    }
}

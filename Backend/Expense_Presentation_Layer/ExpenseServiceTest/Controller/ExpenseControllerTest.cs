using Buisness_Layer.DTOs;
using Buisness_Layer.Services.ExpenseService;
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
    public class ExpenseControllerTest
    {
        private readonly Mock<IExpenseService> _expenseServicemock;
        private readonly ExpenseController _expenseController;
        public ExpenseControllerTest()
        {
            _expenseServicemock = new Mock<IExpenseService>();
            _expenseController=new ExpenseController( _expenseServicemock.Object );
        }
        [Fact]
        public async Task AddExpenseAsync_ReturnsBadRequestOnNull()
        {
            //arrange
            ExpenseDTO expenseDTO = new ExpenseDTO();
            expenseDTO = null;
            int groupId = 1;
            //act
            var result = await _expenseController.AddExpenseAsync(expenseDTO, groupId);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task AddExpenseAsync_ReturnsOKResponse()
        {
            //arrange
            var expenseDTO = new ExpenseDTO() { ExpenseId = 1, ExpenseName = "Test" };
            int groupId = 1;
            _expenseServicemock.Setup(x => x.AddExpenseAsync(expenseDTO, groupId)).ReturnsAsync(expenseDTO);
            //act
            var result = await _expenseController.AddExpenseAsync(expenseDTO, groupId);
            //assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task getMyExpensesPaidByMeAsync_ReturnsBadRequestOnNull()
        {
            //arrange
            int groupId = 0;
            int userId = 0;
            //act
            var result = await _expenseController.getMyExpensesPaidByMeAsync(groupId, userId);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}

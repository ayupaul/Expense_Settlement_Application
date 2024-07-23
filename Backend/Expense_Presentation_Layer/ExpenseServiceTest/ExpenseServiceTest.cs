using Buisness_Layer.DTOs;
using Buisness_Layer.Services.ExpenseService;
using Buisness_Layer.Validation;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.ExpenseRepo;
using Data_Access_Layer.Repository.IRepo;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseServiceTest
{
    public class ExpenseServiceTest
    {
        private readonly Mock<IExpenseRepo> _expenseRepo;
        private readonly Mock<IAccount> _accountRepo;
        private readonly IValidator<ExpenseDTO> _validator;
        private readonly ExpenseService _expenseService;

        public ExpenseServiceTest()
        {
            _expenseRepo = new Mock<IExpenseRepo>();
            _accountRepo = new Mock<IAccount>();
            _validator = new ExpenseValidator();
            _expenseService = new ExpenseService(_expenseRepo.Object, _accountRepo.Object, _validator);
        }
        [Theory]
        [InlineData("", 0, "", "Expense Name is required, Expense Description is required, Expense Amount is required, Expense Date is required, Expense Paid By Emails cannot be empty, Expense Split Among cannot be empty")]
        public async Task AddExpenseAsync_ValidationTest(string expenseName, int expenseAmount, string expenseDescription, string errorMessage)
        {
            //arrange
            var expense = new ExpenseDTO { ExpenseId = 1, ExpenseName = expenseName, ExpenseAmount = expenseAmount, Description = expenseDescription };
            var expenseModel = new ExpenseModel { ExpenseId = 1, ExpenseName = expense.ExpenseName, ExpenseAmount = expense.ExpenseAmount, Description = expense.Description };
            _expenseRepo.Setup(x => x.CreateExpenseAsync(expenseModel)).ReturnsAsync(expenseModel);
            //act
            var result = await _expenseService.AddExpenseAsync(expense, 1);
            //assert
            Assert.Equal(errorMessage, result.Error);
        }
        [Theory]
        [InlineData(99)]
        public async Task GetExpenseByIdAsync_InvalidId_ReturnsNull(int expenseId)
        {
            //arrange
            var expense=new ExpenseModel { ExpenseId = 1,ExpenseName="test",ExpenseAmount=500,ExpenseDate="02/07/2020",Description="test" };
            _expenseRepo.Setup(x=>x.GetExpenseByIdAsync(expenseId)).ReturnsAsync((ExpenseModel)null);
            //act
            var result =await _expenseService.GetExpenseByIdAsync(expenseId);
            //assert
            Assert.Null(result);
        }
        [Theory]
        [InlineData(1)]
        public async Task GetExpenseByIdAsync_ReturnsExpenseModel(int expenseId)
        {
            //arrange
            var expense = new ExpenseModel { ExpenseId = expenseId, ExpenseName = "test", ExpenseAmount = 500, ExpenseDate = "02/07/2020", Description = "test" };
            _expenseRepo.Setup(x => x.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expense);
            //act
            var result = await _expenseService.GetExpenseByIdAsync(expenseId);
            //assert
            Assert.Equal(expenseId, result.ExpenseId);
        }
        [Fact]
        public async Task GetExpensesInMyGroupPaidByMeAsync_Returns_ListOfExpense()
        {
            //arrange
            var expenses = new List<ExpenseModel>()
            {
                new ExpenseModel {ExpenseId=1,ExpenseName="test", ExpenseAmount=500,ExpenseDate="20/07/2000",
                Description="test",GroupId=1}
            };
            _expenseRepo.Setup(x=>x.GetAllExpensesInGroupAsync(1)).ReturnsAsync(expenses);
            _expenseRepo.Setup(repo => repo.CheckExpenseHasPaidUser(1, 1))
            .ReturnsAsync(true);
            _expenseRepo.Setup(repo => repo.CheckExpenseHasPaidUser(1, 2))
                .ReturnsAsync(false);
            //act
            var result = await _expenseService.GetExpensesInMyGroupPaidByMeAsync(1, 1);
            //assert
            Assert.Single(result);
        }
        [Fact]
        public async Task GetExpensesInMyGroupSplitForMeAsync_ReturnsExpense()
        {
            //arrange
            var expenses = new List<ExpenseModel>()
            {
                new ExpenseModel {ExpenseId=1,ExpenseName="test", ExpenseAmount=500,ExpenseDate="20/07/2000",
                Description="test",GroupId=1}
            };
            _expenseRepo.Setup(x => x.GetAllExpensesInGroupAsync(1)).ReturnsAsync(expenses);
            _expenseRepo.Setup(repo => repo.CheckExpenseHasSplitUser(1, 1))
            .ReturnsAsync(true);
            _expenseRepo.Setup(repo => repo.CheckExpenseHasSplitUser(1, 2))
                .ReturnsAsync(false);
            //act
            var result = await _expenseService.GetExpensesInMyGroupSplitForMeAsync(1, 1);
            //assert
            Assert.Single(result);
        }
    }
    
}

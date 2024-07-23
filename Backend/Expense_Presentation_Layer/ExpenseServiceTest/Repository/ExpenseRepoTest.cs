using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.ExpenseRepo;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseServiceTest.Repository
{
    public class ExpenseRepoTest
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public ExpenseRepoTest()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseDb")
                .Options;
        }

        [Fact]
        public async Task AddUserPaidForExpense_ShouldAddExpensePaidByUser()
        {
            // Arrange
            var expenseId = 1;
            var userId = 1;

            using (var context = new AppDbContext(_options))
            {
                var expenseRepo = new ExpenseRepo(context);

                // Act
                await expenseRepo.AddUserPaidForExpense(expenseId, userId);
            }

            // Assert
            using (var context = new AppDbContext(_options))
            {
                var expensePaid = await context.ExpensePaids.FirstOrDefaultAsync(e => e.ExpenseId == expenseId && e.UserId == userId);
                Assert.NotNull(expensePaid);
                Assert.Equal(expenseId, expensePaid.ExpenseId);
                Assert.Equal(userId, expensePaid.UserId);
            }
        }
        [Fact]
        public async Task AddUserSplitAmongExpense_ShouldAddUserSplitAmongUser()
        {
            //arrange
             int expenseId = 1;
             int userId = 1;
            int amountOwe = 100;
            using(var context = new AppDbContext(_options))
            {
                var expenseRepo=new ExpenseRepo(context);
                //act
                await expenseRepo.AddUserSplitAmongExpense(expenseId,userId,amountOwe);
               

            }
            //assert
            using (var context = new AppDbContext(_options))
            {
                var expenseSplit = await context.ExpenseSplits.FirstOrDefaultAsync(e => e.ExpenseId == expenseId && e.UserId == userId);
                Assert.NotNull(expenseSplit);
                Assert.Equal(expenseId, expenseSplit.ExpenseId);
                Assert.Equal(userId, expenseSplit.UserId);
            }
        }
        [Fact]
        public async Task CreateExpenseAsync_ShouldCreateExpense()
        {
            //assert
            var expense = new ExpenseModel { ExpenseName = "test", ExpenseId = 1 };
            using (var context= new AppDbContext(_options))
            {
                var expenseRepo = new ExpenseRepo(context);
                //act
                await expenseRepo.CreateExpenseAsync(expense);
            }
            //assert
            using(var context=new AppDbContext(_options))
            {
                var expenseDetail = await context.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == 1);
                Assert.NotNull(expenseDetail);
                Assert.Equal(expense.ExpenseId, expenseDetail.ExpenseId);
            }
        }

    }
}

using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.ExpenseRepo
{
    public class ExpenseRepo : IExpenseRepo
    {
        private readonly AppDbContext appDbContext;

        public ExpenseRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddUserPaidForExpense(int expenseId, int userId)
        {
            var userExpensePaid= new ExpensePaidByUserModel() { ExpenseId = expenseId, UserId = userId };
            await appDbContext.ExpensePaids.AddAsync(userExpensePaid);
            await appDbContext.SaveChangesAsync();
        }

        public async Task AddUserSplitAmongExpense(int expenseId, int userId,int amountOwe)
        {
            var userExpenseSplit=new ExpenseSplitAmongUserModel() { ExpenseId = expenseId,UserId = userId,AmountOwe=amountOwe};
            await appDbContext.ExpenseSplits.AddAsync(userExpenseSplit);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckExpenseHasPaidUser(int userId,int expenseId)
        {
            var expense=await appDbContext.ExpensePaids.FirstOrDefaultAsync(x=>x.UserId == userId && x.ExpenseId==expenseId);
            if (expense!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckExpenseHasSplitUser(int userId, int expenseId)
        {
            var expense = await appDbContext.ExpenseSplits.FirstOrDefaultAsync(x => x.UserId == userId && x.ExpenseId == expenseId);
            if (expense!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ExpenseSplitAmongUserModel>> GetExpensesInSplitByIdAsync(int expenseId)
        {
            var expense = await appDbContext.ExpenseSplits.Where(x => x.ExpenseId == expenseId).ToListAsync();
            return expense;
        }

        public async Task<ExpenseModel> CreateExpenseAsync(ExpenseModel expense)
        {
            await appDbContext.Expenses.AddAsync(expense);
            await appDbContext.SaveChangesAsync();

            return expense;
        }

        public async Task<ICollection<ExpenseModel>> GetAllExpensesInGroupAsync(int groupId)
        {
            var expense = await appDbContext.Groups.Include(u => u.Expenses).FirstOrDefaultAsync(g => g.GroupId == groupId);
            var expenses = expense.Expenses;
            return expenses;
           
        }

        public async Task<List<ExpensePaidByUserModel>> getAllPaidExpensesById(int expenseId)
        {
            var expenses=await appDbContext.ExpensePaids.Where(x=>x.ExpenseId==expenseId).ToListAsync();
            return expenses;
        }

        public async Task<int> getAmountOweAsync(int expenseId, int userId)
        {
            var expenseSplit=await appDbContext.ExpenseSplits.FirstOrDefaultAsync(x=>x.UserId==userId && x.ExpenseId==expenseId);
            var oweAmount = expenseSplit.AmountOwe;
            return oweAmount;
        }

        public async Task<ExpenseModel?> GetExpenseByIdAsync(int expenseId)
        {
            var expense = await appDbContext.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == expenseId);
            return expense;
        }

        public async Task<ExpenseModel> GetMyExpenseById(int expenseId)
        {
            var expense=await appDbContext.Expenses.FirstOrDefaultAsync(x=>x.ExpenseId==expenseId);
            return expense;
        }

        public async Task RemoveExpenseSettled(int expenseId, int userId)
        {
            var expense = await appDbContext.ExpenseSplits.FirstOrDefaultAsync(x => x.ExpenseId == expenseId && x.UserId == userId);
            if (expense != null)
            {
                appDbContext.ExpenseSplits.Remove(expense);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task CloseExpenseAsync(int expenseId)
        {
            var expensesPaid = await appDbContext.ExpensePaids.Where(x => x.ExpenseId == expenseId).ToListAsync();
            if(expensesPaid.Count > 0)
            {
                foreach(var expensePaid in expensesPaid)
                {
                    appDbContext.ExpensePaids.Remove(expensePaid);
                    await appDbContext.SaveChangesAsync();
                }
            }
            var expense = await appDbContext.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == expenseId);
            if(expense != null)
            {
                appDbContext.Expenses.Remove(expense);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}

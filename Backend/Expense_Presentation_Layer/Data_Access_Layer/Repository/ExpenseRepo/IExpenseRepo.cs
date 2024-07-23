using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data_Access_Layer.Repository.ExpenseRepo
{
    public interface IExpenseRepo
    {
        Task<ExpenseModel> CreateExpenseAsync(ExpenseModel expense);
        Task AddUserPaidForExpense(int expenseId,int userId);
        Task AddUserSplitAmongExpense(int expenseId,int userId,int amountOwe);
        Task<ICollection<ExpenseModel>> GetAllExpensesInGroupAsync(int groupId);
        Task<bool> CheckExpenseHasPaidUser(int userId,int expenseId);
        Task<bool> CheckExpenseHasSplitUser(int userId,int expenseId);
        Task<ExpenseModel> GetMyExpenseById(int expenseId);
        Task<int> getAmountOweAsync(int expenseId, int userId);
        Task<List<ExpensePaidByUserModel>> getAllPaidExpensesById(int expenseId);
        Task RemoveExpenseSettled(int expenseId,int userId);
        Task<ExpenseModel?> GetExpenseByIdAsync(int expenseId);
        Task<List<ExpenseSplitAmongUserModel>> GetExpensesInSplitByIdAsync(int expenseId);
        Task CloseExpenseAsync(int expenseId);
    }
}

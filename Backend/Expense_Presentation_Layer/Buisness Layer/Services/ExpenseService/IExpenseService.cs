using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expense,int groupId);
        Task<List<ExpenseDTO>> GetExpensesInMyGroupPaidByMeAsync(int groupId,int userId);
        Task<List<ExpenseDTO>> GetExpensesInMyGroupSplitForMeAsync(int groupId, int userId);
        Task<ExpenseDTO> ViewMyOweExpenseDetails(int expenseId,int userId);
        Task SettleAmountOwed(int expenseId,int userId,int amountOwe);
        Task<ExpenseModel?> GetExpenseByIdAsync(int expenseId);
        Task<bool> CheckExpenseSettled(int expenseId);
        Task CloseExpenseAsync(int expenseId);
    }
}

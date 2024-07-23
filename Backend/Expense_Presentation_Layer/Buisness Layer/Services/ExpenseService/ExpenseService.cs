using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.ExpenseRepo;
using Data_Access_Layer.Repository.IRepo;
using FluentValidation;


namespace Buisness_Layer.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepo expenseRepo;
        private readonly IAccount account;
        private readonly IValidator<ExpenseDTO> validator;

        public ExpenseService(IExpenseRepo expenseRepo,IAccount account,IValidator<ExpenseDTO> validator)
        {
            this.expenseRepo = expenseRepo;
            this.account = account;
            this.validator = validator;
        }
        public async Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expense,int groupId)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(expense);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                var expenseDetails = new ExpenseModel() { Description = expense.Description, ExpenseName = expense.ExpenseName, ExpenseAmount = expense.ExpenseAmount, ExpenseDate = expense.ExpenseDate, GroupId = groupId };
                var paidEmails = expense.EmailsPaidBy;
                var splitEmails = expense.EmailSplitAmongs;
                var expenseModel = await expenseRepo.CreateExpenseAsync(expenseDetails);
                if (expenseModel == null)
                {
                    throw new ArgumentNullException("Something went wrong while creating exception");
                }
                var amount = expenseModel.ExpenseAmount / paidEmails.Count;
                var amountOwe = expenseModel.ExpenseAmount / splitEmails.Count;
                foreach (var email in paidEmails)
                {
                    var user = await account.GetUserByEmailAsync(email);
                    await expenseRepo.AddUserPaidForExpense(expenseModel.ExpenseId, user.Id);
                    await account.UpdateAmountAsync(user, amount);
                }
                foreach (var email in splitEmails)
                {
                    var user = await account.GetUserByEmailAsync(email);
                    await expenseRepo.AddUserSplitAmongExpense(expenseModel.ExpenseId, user.Id, amountOwe);
                }
                return new ExpenseDTO { ExpenseName = expenseModel.ExpenseName, ExpenseAmount = expenseModel.ExpenseAmount, ExpenseDate = expenseModel.ExpenseDate, ExpenseId = expenseModel.ExpenseId };

            }
            catch (ValidationException ex)
            {
                var errors = string.Join(", ", ex.Errors.Select(x => x.ErrorMessage));
                return new ExpenseDTO { Error = errors };
            }
            catch (ArgumentNullException ex)
            {
                return new ExpenseDTO { Error = ex.Message };
            }
        }

        public async Task<bool> CheckExpenseSettled(int expenseId)
        {
            var expenses=await expenseRepo.GetExpensesInSplitByIdAsync(expenseId);
            
            if(expenses.Count>0)
            {
                return false;
            }
            return true;
        }

        public async Task CloseExpenseAsync(int expenseId)
        {
            await expenseRepo.CloseExpenseAsync(expenseId);

        }

        public async Task<ExpenseModel?> GetExpenseByIdAsync(int expenseId)
        {
            var expense=await expenseRepo.GetExpenseByIdAsync(expenseId);
            if (expense == null)
            {
                return null;
            }
            return expense;
        }

        public async Task<List<ExpenseDTO>> GetExpensesInMyGroupPaidByMeAsync(int groupId, int userId)
        {
            var expenses = await expenseRepo.GetAllExpensesInGroupAsync(groupId);
            var myExpenses=new List<ExpenseDTO>();
            foreach(var expense in expenses)
            {
                var expenseDetail=new ExpenseDTO() { ExpenseId=expense.ExpenseId,ExpenseName=expense.ExpenseName};
                var isExpensePresentInPaid = await expenseRepo.CheckExpenseHasPaidUser(userId, expense.ExpenseId);
    
                if(isExpensePresentInPaid)
                {
                    myExpenses.Add(expenseDetail);
                }
            }
            return myExpenses;
        }

        public async Task<List<ExpenseDTO>> GetExpensesInMyGroupSplitForMeAsync(int groupId, int userId)
        {
            var expenses = await expenseRepo.GetAllExpensesInGroupAsync(groupId);
            var myExpenses = new List<ExpenseDTO>();
            foreach (var expense in expenses)
            {
                var expenseDetail = new ExpenseDTO() { ExpenseId = expense.ExpenseId, ExpenseName = expense.ExpenseName };
                var isExpensePresentInSplit=await expenseRepo.CheckExpenseHasSplitUser(userId, expense.ExpenseId);
                if (isExpensePresentInSplit)
                {
                    myExpenses.Add(expenseDetail);
                }
            }
            return myExpenses;
        }

        public async Task SettleAmountOwed(int expenseId, int userId,int amountOwe)
        {
            var user=await account.GetUserByIdAsync(userId);
            await account.UpdateAmountAsync(user, amountOwe);
            var expenses=await expenseRepo.getAllPaidExpensesById(expenseId);
            int amountToOwners=amountOwe/expenses.Count;
            foreach (var expense in expenses)
            {
                await account.settleAmountAsync(amountToOwners, expense.UserId);
            }
            await expenseRepo.RemoveExpenseSettled(expenseId, userId);
        }

        public async Task<ExpenseDTO> ViewMyOweExpenseDetails(int expenseId, int userId)
        {
            var expense=await expenseRepo.GetMyExpenseById(expenseId);
            var amountOwe=await expenseRepo.getAmountOweAsync(expenseId, userId);
            var expenseDetails = new ExpenseDTO()
            {
                ExpenseId = expense.ExpenseId,
                AmountOwe = amountOwe,
                ExpenseName = expense.ExpenseName,
                ExpenseAmount = expense.ExpenseAmount,
                ExpenseDate = expense.ExpenseDate,
                Description = expense.Description,
            };
            return expenseDetails;
        }
    }
}

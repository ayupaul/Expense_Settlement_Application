using Buisness_Layer.DTOs;
using Buisness_Layer.Services.ExpenseService;
using Data_Access_Layer.Repository.ExpenseRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;
       

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
          
        }
        [HttpPost("{groupId}")]
        [Authorize]
        public async Task<IActionResult> AddExpenseAsync(ExpenseDTO expenseDTO, [FromRoute] int groupId)
        {
            if (expenseDTO == null)
            {
                return BadRequest();
            }
            var expense=await expenseService.AddExpenseAsync(expenseDTO,groupId);
            return Ok(expense);
        }
        [HttpGet("getMyExpensesPaidByMe/{groupId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> getMyExpensesPaidByMeAsync([FromRoute] int groupId, [FromRoute] int userId)
        {
            if(userId==0 || groupId == 0)
            {
                return BadRequest();
            }
            var expenses=await expenseService.GetExpensesInMyGroupPaidByMeAsync(groupId, userId);
            return Ok(expenses);
        }
        [HttpGet("getMyExpensesSplitForMe/{groupId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> getMyExpensesSplitForMeAsync([FromRoute] int groupId, [FromRoute] int userId)
        {
            if(userId==0 || groupId == 0)
            {
                return BadRequest();
            }
            var expenses=await expenseService.GetExpensesInMyGroupSplitForMeAsync(groupId, userId);
            return Ok(expenses);
        }
        [HttpGet("getMyOwedExpense/{expenseId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> getMyOweExpense([FromRoute] int expenseId, [FromRoute] int userId)
        {
            if(expenseId==0 || userId == 0)
            {
                return BadRequest();
            }
            var expense = await expenseService.ViewMyOweExpenseDetails(expenseId, userId);
            return Ok(expense);
        }
        [HttpPost("settleExpense/{expenseId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> settleExpense([FromRoute] int expenseId, [FromRoute] int userId, [FromBody] int amountOwe)
        {
            if(expenseId==0 || userId==0 || amountOwe == 0)
            {
                return BadRequest();
            }
            await expenseService.SettleAmountOwed(expenseId,userId,amountOwe);
            return Ok();
        }
        [HttpGet("getExpenseById/{expenseId}")]
        [Authorize]
        public async Task<IActionResult> getExpenseByIdAsync([FromRoute] int expenseId)
        {
            if (expenseId == 0)
            {
                return BadRequest();
            }
            var expense=await expenseService.GetExpenseByIdAsync(expenseId);
            return Ok(expense);
        }
        [HttpGet("checkExpenseSettled/{expenseId}")]
        [Authorize]
        public async Task<IActionResult> checkExpenseSettledAsync([FromRoute] int expenseId)
        {
            if(expenseId == 0)
            {
                return BadRequest();
            }
            bool isSettled = await expenseService.CheckExpenseSettled(expenseId);
            return Ok(isSettled);
        }
        [HttpDelete("closeExpense/{expenseId}")]
        [Authorize]
        public async Task<IActionResult> CloseExpenseAsync([FromRoute] int expenseId)
        {
            if (expenseId == 0)
            {
                return BadRequest();
            }
            await expenseService.CloseExpenseAsync(expenseId);
            return Ok();
        }
    }
}

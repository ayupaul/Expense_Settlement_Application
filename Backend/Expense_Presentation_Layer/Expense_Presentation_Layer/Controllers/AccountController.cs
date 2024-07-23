using Buisness_Layer.DTOs;
using Buisness_Layer.Services.AccountService;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserModel userModel)
        {
            var Email=userModel.Email;
            var Password=userModel.Password;
            var user=await accountService.LoginAsync(Email, Password);
            if (user == null)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(user.Error))
            {
                return StatusCode(500, user.Error);
            }
            return Ok(user);
        }
        [HttpGet("getAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users=await accountService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("getUserById/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            if(userId == 0)
            {
                return BadRequest();
            }
            var user= await accountService.GetUserByIdAsync(userId);
            return Ok(user);
        }
        [HttpGet("getAllUserForAdmin")]
        [Authorize]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await accountService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPut("updateUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(UserModel user, int userId)
        {
            if(user== null || userId==0)
            {
                return BadRequest();
            }
            await accountService.UpdateAsync(user,userId);
            return Ok();
        }

    }
}

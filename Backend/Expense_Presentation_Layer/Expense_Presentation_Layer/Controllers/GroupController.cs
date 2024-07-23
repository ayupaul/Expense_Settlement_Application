using Buisness_Layer.Services.GroupService;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }
        [HttpPost("{userId}")]
        [Authorize]
        public async Task<IActionResult> CreateGroupAsync([FromBody] GroupModel group,[FromRoute] int userId)
        {
            if(group == null || userId==0)
            {
                return BadRequest();
            }
            var groupDetail=await groupService.CreateGroupAsync(group, userId);
            if(groupDetail == null)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(groupDetail.Error))
            {
                return StatusCode(500,groupDetail.Error);
            }
            return Ok(groupDetail);
        }
        [HttpGet("getMyGroups/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetMyGroupsAsync([FromRoute] int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }
            var groups=await groupService.GetMyGroupsAsync(userId);
            return Ok(groups);
        }
        [HttpGet("getUsersInGroup/{groupId}")]
        [Authorize]
        public async Task<IActionResult> GetUsersInGroupAsync([FromRoute] int groupId)
        {
            if (groupId == 0)
            {
                return BadRequest();
            }
            var users=await groupService.GetUsersInGroupAsync(groupId);
            return Ok(users);
        }
        [HttpPost("addUserToGroup/{userId}")]
        [Authorize]
        public async Task<IActionResult> AddUserToGroupAsync([FromRoute] int userId, [FromBody] int groupId)
        {
            if(userId==0 || groupId==0)
            {
                return BadRequest();
            }
            var isUserAddedToGroup= await groupService.AddUserToGroupAsync(userId, groupId);
            if(isUserAddedToGroup)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Maximum Length Exceed");
            }
        }
        
    }
}

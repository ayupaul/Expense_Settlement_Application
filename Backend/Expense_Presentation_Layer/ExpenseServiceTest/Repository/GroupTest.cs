using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.GroupRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseServiceTest.Repository
{
    public class GroupTest
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public GroupTest()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "ExpenseDb")
               .Options;
        }
        [Fact]
        public async Task CreateGroupAsync_ShouldCreateGroup()
        {
            //arrange
            GroupModel groupModel = new GroupModel() { GroupId = 1, GroupName = "test" };
            int userId = 1;
            //act
            using (var context=new AppDbContext(_options))
            {
                var groupRepo=new GroupRepo(context);
                //act
                await groupRepo.CreateGroupAsync(groupModel, userId);
                
            }
            //assert
            using (var context=new AppDbContext(_options))
            {
                var group=await context.Groups.FirstOrDefaultAsync(x=>x.GroupId==groupModel.GroupId);
                Assert.NotNull(group);
            }
        }
    }
}

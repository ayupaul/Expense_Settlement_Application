using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<UserGroupModel> UserGroups { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<ExpensePaidByUserModel> ExpensePaids { get; set; }
        public DbSet<ExpenseSplitAmongUserModel> ExpenseSplits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<UserModel> users = new List<UserModel>()
            {
                new UserModel() {Id=1,UserName="Ayush",Email="ayush@gmail.com",Password="Ayush123@",Amount=1000,Role="Admin"},
                new UserModel() {Id=2,UserName="Abhishek",Email="abhishek@gmail.com",Password="Abhishek@123",Amount=1000,Role="User"},
                new UserModel() {Id=3,UserName="Ankit",Email="ankit@gmail.com",Password="Ankit123@",Amount=1000,Role="User"},
                new UserModel() {Id=4,UserName="Anamika",Email="anamika@gmail.com",Password="Anamika123@",Amount=1000, Role = "User"},
                new UserModel() {Id=5,UserName="Admin",Email="admin@gmail.com",Password="Admin123@",Amount=1000, Role = "Admin"}
            };
            foreach (var user in users)
            {
                modelBuilder.Entity<UserModel>().HasData(user);
            }
            
           
        }

    }
}

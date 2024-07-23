﻿// <auto-generated />
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data_Access_Layer.Models.ExpenseModel", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpenseAmount")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.HasIndex("GroupId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ExpensePaidByUserModel", b =>
                {
                    b.Property<int>("ExpensePaidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpensePaidId"), 1L, 1);

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ExpensePaidId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("ExpensePaids");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ExpenseSplitAmongUserModel", b =>
                {
                    b.Property<int>("ExpenseSplitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseSplitId"), 1L, 1);

                    b.Property<int>("AmountOwe")
                        .HasColumnType("int");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseSplitId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("ExpenseSplits");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.GroupModel", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.UserGroupModel", b =>
                {
                    b.Property<int>("UserGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGroupId"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserGroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1000,
                            Email = "ayush@gmail.com",
                            Password = "Ayush123@",
                            Role = "Admin",
                            UserName = "Ayush"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 1000,
                            Email = "abhishek@gmail.com",
                            Password = "Abhishek@123",
                            Role = "User",
                            UserName = "Abhishek"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1000,
                            Email = "ankit@gmail.com",
                            Password = "Ankit123@",
                            Role = "User",
                            UserName = "Ankit"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 1000,
                            Email = "anamika@gmail.com",
                            Password = "Anamika123@",
                            Role = "User",
                            UserName = "Anamika"
                        },
                        new
                        {
                            Id = 5,
                            Amount = 1000,
                            Email = "admin@gmail.com",
                            Password = "Admin123@",
                            Role = "Admin",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ExpenseModel", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.GroupModel", "Group")
                        .WithMany("Expenses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ExpensePaidByUserModel", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.ExpenseModel", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ExpenseSplitAmongUserModel", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.ExpenseModel", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.UserGroupModel", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.GroupModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.GroupModel", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}

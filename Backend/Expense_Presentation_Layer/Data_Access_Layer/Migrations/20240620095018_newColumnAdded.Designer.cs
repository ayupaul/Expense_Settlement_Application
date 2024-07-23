﻿// <auto-generated />
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240620095018_newColumnAdded")]
    partial class newColumnAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
                        .IsRequired()
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
#pragma warning restore 612, 618
        }
    }
}

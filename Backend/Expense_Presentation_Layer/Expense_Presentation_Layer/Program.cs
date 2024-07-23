using AutoMapper;
using Buisness_Layer.AutoMapper;
using Buisness_Layer.DTOs;
using Buisness_Layer.Services.AccountService;
using Buisness_Layer.Services.ExpenseService;
using Buisness_Layer.Services.GroupService;
using Buisness_Layer.Validation;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.ExpenseRepo;
using Data_Access_Layer.Repository.GroupRepo;
using Data_Access_Layer.Repository.IRepo;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseDBConnection")));
builder.Services.AddScoped<IAccount,Account>();
builder.Services.AddScoped<IGroupRepo, GroupRepo>();
builder.Services.AddScoped<IExpenseRepo, ExpenseRepo>();

builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IExpenseService,ExpenseService>();

//registering of validators
builder.Services.AddScoped<IValidator<UserModel>,AccountValidator>();
builder.Services.AddScoped<IValidator<GroupModel>,GroupValidator>();
builder.Services.AddScoped<IValidator<ExpenseDTO>,ExpenseValidator>();

builder.Services.AddSingleton<IMapper>(MappingConfig.Configure());
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
var app = builder.Build();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

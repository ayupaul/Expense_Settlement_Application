using AutoMapper;
using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.IRepo;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Buisness_Layer.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccount account;
        private readonly IValidator<UserModel> validator;
        private readonly IMapper mapper;

        public AccountService(IAccount account,IValidator<UserModel> validator,IMapper mapper)
        {
            this.account = account;
            this.validator = validator;
            this.mapper = mapper;
        }

       

        public async Task<UserDTO?> LoginAsync(string email, string password)
        {
          
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException("Email or Password cannot be null");
                }
               var validationResult=await validator.ValidateAsync(new UserModel { Email = email, Password = password }); 
                if (!validationResult.IsValid)
                {
                    throw new ValidationException("Validation failed",validationResult.Errors);
                }
                var user = await account.VerifyUserAsync(email, password);
                if (user == null)
                {
                    return null;
                }

                string token;
                try
                {
                     token = CreateJwt(user);
                }
                catch (Exception ex)
                {
                    return new UserDTO { Error="Something went wrong during creating of token" };
                }
                var userData = mapper.Map<UserDTO>(user);
                userData.Token = token;
                return userData;

            }

            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return new UserDTO { Error = "Email or Password cannot be null"};
            }
            catch (ValidationException ex)
            {
                
                var errors = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage));
                Console.WriteLine(errors);
                return new UserDTO { Error = errors };
            }
            

        }
        public string CreateJwt(UserModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {

                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name,user.Email),
                 new Claim("UserId", user.Id.ToString()),
                 new Claim("Amount",user.Amount.ToString()),
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users=await account.GetAllUsersAsync();
            return users;
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user=await account.GetUserByIdAsync(id);
            return user;
        }

        public async Task UpdateAsync(UserModel user,int userId)
        {
            await account.UpdateUserAsync(user,userId);
        }
    }
}

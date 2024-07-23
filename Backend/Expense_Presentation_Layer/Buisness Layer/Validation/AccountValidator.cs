using Data_Access_Layer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Validation
{
    public class AccountValidator : AbstractValidator<UserModel>
    {
        public AccountValidator()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email Address is not in format");
            RuleFor(u => u.Password).MinimumLength(8).WithMessage("Password minimum length should be 8");
        }
    }
}

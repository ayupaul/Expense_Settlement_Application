using Data_Access_Layer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Validation
{
    public class GroupValidator:AbstractValidator<GroupModel>
    {
        public GroupValidator()
        {
            RuleFor(u => u.GroupName).NotEmpty().WithMessage("Group Name cannot be empty");
            RuleFor(u => u.Description).NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(u => u.CreatedDate).NotEmpty().WithMessage("Created Date cannot be empty");
        }
    }
}

using Buisness_Layer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.Validation
{
    public class ExpenseValidator:AbstractValidator<ExpenseDTO>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.ExpenseName).NotEmpty().WithMessage("Expense Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Expense Description is required");
            RuleFor(x => x.ExpenseAmount).NotEmpty().WithMessage("Expense Amount is required");
            RuleFor(x => x.ExpenseDate).NotEmpty().WithMessage("Expense Date is required");
            RuleFor(x => x.EmailsPaidBy).NotEmpty().WithMessage("Expense Paid By Emails cannot be empty");
            RuleFor(x => x.EmailSplitAmongs).NotEmpty().WithMessage("Expense Split Among cannot be empty");
        }
    }
}

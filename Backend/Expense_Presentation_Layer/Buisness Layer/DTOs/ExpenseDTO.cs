using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.DTOs
{
    public class ExpenseDTO
    {
        public int ExpenseId { get; set; }
        public string? ExpenseName { get; set; }
        public int ExpenseAmount { get; set; }
        public string? Description { get; set; }
        public string? ExpenseDate { get; set; }
        public List<string>? EmailsPaidBy { get; set; }
        public List<string>? EmailSplitAmongs { get; set; }
        public string? Error { get; set; }
        public int AmountOwe { get; set; }
    }
}

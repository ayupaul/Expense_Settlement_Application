using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ExpensePaidByUserModel
    {
        [Key]
        public int ExpensePaidId { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public int ExpenseId { get; set; }
        public  ExpenseModel? Expense { get; set; }
    }
}

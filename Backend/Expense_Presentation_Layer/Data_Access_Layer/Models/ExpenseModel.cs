using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ExpenseModel
    {
        [Key]
        public int ExpenseId { get; set; }
        public string? ExpenseName { get; set; }
        public int ExpenseAmount { get; set; }
        public string? Description { get; set; }
        public string? ExpenseDate { get; set; }
        public int GroupId { get; set; }
        public GroupModel? Group { get; set; }
    }
}

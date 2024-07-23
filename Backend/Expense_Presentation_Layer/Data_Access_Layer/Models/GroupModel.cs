using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class GroupModel
    {
        [Key]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public string? CreatedDate { get; set; }
        public ICollection<ExpenseModel>? Expenses { get; set; }
    }
}

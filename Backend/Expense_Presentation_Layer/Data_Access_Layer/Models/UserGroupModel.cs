using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class UserGroupModel
    {
        [Key]
        public int UserGroupId { get; set; }
        public int UserId { get; set; }
        public  UserModel? User { get; set; }
        public int GroupId { get; set; }
        public  GroupModel? Group { get; set; }
    }
}

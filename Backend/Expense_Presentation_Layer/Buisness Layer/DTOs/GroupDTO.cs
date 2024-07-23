using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.DTOs
{
    public class GroupDTO
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public string? CreatedDate { get; set; }
        public string? Error { get; set; }
    }
}

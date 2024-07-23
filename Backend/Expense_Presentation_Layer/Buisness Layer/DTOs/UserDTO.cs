﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public int Amount { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
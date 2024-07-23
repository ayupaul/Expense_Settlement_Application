using AutoMapper;
using Buisness_Layer.DTOs;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Layer.AutoMapper
{
    public class MappingConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<GroupModel, GroupDTO>();
            });

            return config.CreateMapper();
        }
    }
}

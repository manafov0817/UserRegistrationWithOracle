using AutoMapper;
using OracleTask.Entity.Entities;
using OracleTask.Models;

namespace OracleTask.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserDTO2, User>().ReverseMap();

        }

    }
}

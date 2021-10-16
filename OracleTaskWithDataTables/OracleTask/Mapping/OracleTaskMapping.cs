using AutoMapper;
using OracleTask.Entity.Entities;
using OracleTask.Models;

namespace OracleTask.Mapping
{
    public class OracleTaskMapping : Profile
    {
        public OracleTaskMapping()
        {
            CreateMap<UserDTOIndex, User>().ReverseMap();
            CreateMap<UserDTOCreate, User>().ReverseMap();

            CreateMap<CityDTO, City>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();

        }

    }
}

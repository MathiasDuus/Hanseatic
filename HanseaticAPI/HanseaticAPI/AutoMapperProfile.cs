using AutoMapper;
using HanseaticAPI.Models;

namespace HanseaticAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityProduct, CityProductDTO>();
            CreateMap<CityProductDTO, CityProduct>();
        }

    }
}

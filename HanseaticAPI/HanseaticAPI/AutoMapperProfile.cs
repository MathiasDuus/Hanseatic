using HanseaticAPI.Models;

namespace HanseaticAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<CityProduct, CityProductDTO>();
            CreateMap<CityProductDTO, CityProduct>();


            CreateMap<ProductType, ProductTypeDTO>();
            CreateMap<ProductTypeDTO, ProductType>();
        }

    }
}

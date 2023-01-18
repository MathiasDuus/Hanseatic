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

            CreateMap<Ship, ShipDTO>();
            CreateMap<ShipDTO, Ship>();

            CreateMap<ShipProduct, ShipProductDTO>();
            CreateMap<ShipProductDTO, ShipProduct>();

            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            CreateMap<Save, SaveDTO>();
            CreateMap<SaveDTO, Save>();
        }

    }
}

using AutoMapper;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.User.Domain;

namespace CarMarket.Data.Configuration.Mapping
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<CarEntity, CarModel>().ReverseMap();
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}
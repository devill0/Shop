using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Services;
using System;

namespace Shop.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<User, UserDTO>().ForMember(m => m.Role, o => o.MapFrom(p =>
                    (RoleDTO)Enum.Parse(typeof(RoleDTO), p.Role.ToString(), true)));
            }).CreateMapper();
    }
}

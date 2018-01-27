using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Web.Models;
using System;

namespace Shop.Web.Framework
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, CartDTO>();
                cfg.CreateMap<CartItem, CartItemDTO>();
                cfg.CreateMap<CartDTO, CartViewModel>();
                cfg.CreateMap<CartItemDTO, CartItemViewModel>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<User, UserDTO>().ForMember(m => m.Role, o => o.MapFrom(p =>
                    (RoleDTO)Enum.Parse(typeof(RoleDTO), p.Role.ToString(), true)));
            }).CreateMapper();
    }
}

﻿using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;

namespace Shop.Service.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {                
                cfg.CreateMap<Product, ProductDTO>();                         
            }).CreateMapper();
    }
}

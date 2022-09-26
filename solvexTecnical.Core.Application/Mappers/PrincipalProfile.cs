using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Domain.Entities;

namespace solvexTecnical.Core.Application.Mappers
{
    public class PrincipalProfile: Profile
    {
        public PrincipalProfile()
        {
            CreateMap<ShoppingListProducts, ShoppingListDTO>().ReverseMap();
            CreateMap<SuperMarket, SuperMarketDTO>().ReverseMap();
            CreateMap<ShoppingList, ShoppingListDTO>().ReverseMap();
            CreateMap<ProductsBrands, BrandDTO>().ReverseMap();
            CreateMap<Products, ProductDTO>().ReverseMap();
            CreateMap<FinalProducts, FinalProductDTO>().ReverseMap();
            CreateMap<Products, FinalProductDTO>().ReverseMap();
            CreateMap<Users, UserDTO>().ReverseMap();

        }

    }
}

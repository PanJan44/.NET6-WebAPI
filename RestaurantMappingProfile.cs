﻿using AutoMapper;
using web_api_net5.Entities;
using web_api_net5.Models;

namespace web_api_net5
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            //te które się tak samo nazywają zostaną zmapowane automatycznie
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                    c => c.MapFrom(dto => new Address()
                    { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<CreateDishDto, Dish>();
        }
    }
}

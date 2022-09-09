using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using web_api_net5.Entities;
using web_api_net5.Exceptions;
using web_api_net5.Models;

namespace web_api_net5.Services
{
    public interface IDishService
    {
        int Create(int id, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
        void DeleteAll(int restaurantId);
        void DeleteById(int restaurantId, int dishId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            _dbContext.Dishes.Add(dishEntity);
            _dbContext.SaveChanges();
            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var dish = GetDishById(restaurantId, dishId);

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;

        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurants = GetRestaurantById(restaurantId);
            var dishesDto = _mapper.Map<List<DishDto>>(restaurants.Dishes);
            return dishesDto;
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _dbContext.Dishes.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int restaurantId, int dishId)
        {
            var dish = GetDishById(restaurantId, dishId);

            _dbContext.Remove(dish);
            _dbContext.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            return restaurant;
        }

        private Dish GetDishById(int restaurantId, int dishId)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish == null || dish.RestaurantId != restaurantId)
                throw new NotFoundException("Dish not found");
            return dish;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using web_api_net5.Entities;
using web_api_net5.Models;
using web_api_net5.Services;
using static web_api_net5.Services.RestaurantService;

namespace web_api_net5.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        //private readonly RestaurantDbContext _dbContext;
        //private readonly IMapper _mapper;


        private readonly IRestaurantService _restaurantService;


        //te argumenty były przed wydzieleniem logiki do osobnego serwisu
        public RestaurantController(/*RestaurantDbContext dbContext, IMapper mapper*/IRestaurantService restaurantService)
        {
            //_dbContext = dbContext;
            //_mapper = mapper;
            _restaurantService = restaurantService;
        }

        //pobieranie wszystkich restauracji 
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            //-------PRZED UTWORZENIEM KLAS DTO
            //var restaurants = _dbContext 
            //    .Restaurants
            //    .ToList();

            //return Ok(restaurants);

            //var restaurants = _dbContext
            //    .Restaurants
            //    .Include(r => r.Address)
            //    .Include(r => r.Dishes)
            //    .ToList();

            //------TAK TO SIĘ ROBI BEZ AUTOMAPPERA
            //var restaurantsDtos = restaurants.Select(r => new RestaurantDto()
            //{
            //    Name = r.Name,
            //    Category = r.Category,
            //    City = r.Address.City
            //});

            //var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurantDto = _restaurantService.GetById(id);
            return Ok(restaurantDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var id = _restaurantService.Create(dto);

            return Created("/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState); Dzięki atrybutowi [ApiController] nad całą klasą kontrolera to jest niepotrzebne
            _restaurantService.Update(id, dto);
            return Ok();
        }
    }
}

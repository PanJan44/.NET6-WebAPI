using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using web_api_net5.Models;
using web_api_net5.Services;

namespace web_api_net5.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDishId = _service.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = _service.GetById(restaurantId, dishId);

            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            var dishes = _service.GetAll(restaurantId);
            return Ok(dishes);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _service.DeleteAll(restaurantId);
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public ActionResult DeleteById([FromRoute] int restaurantId, [FromRoute]int dishId)
        {
            _service.DeleteById(restaurantId, dishId);
            return NoContent();
        }
    }
}

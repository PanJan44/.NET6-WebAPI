using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_net5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var result = _service.Get();
        //    return result;
        //}

        //[HttpGet("currentDay/{max}")] //2. sposób 
        ////[Route("currentDay")] - 1. sposób
        //public IEnumerable<WeatherForecast> Get2([FromQuery] int take, [FromRoute] int max)
        //{
        //    var result = _service.Get();
        //    return result;
        //}

        //[HttpPost]
        //public ActionResult<string> Hello([FromBody] string name)
        //{
        //    //HttpContext.Response.StatusCode = 401;
        //    //return $"hello {name}";

        //    //return StatusCode(401, $"hello {name}");

        //    return NotFound($"hello {name}");
        //}

        [HttpPost("generate")]
        public ActionResult<IEnumerable<IWeatherForecastService>> Generate([FromQuery] int count, [FromBody] TemperatureRequest request)
        {
            if (count < 0 || request.Min > request.Max)
            {
                return BadRequest();
            }

            var result = _service.Get(count, request.Min, request.Max);
            return Ok(result); //200
        }

    }
}

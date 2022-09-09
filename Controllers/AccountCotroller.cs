using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_net5.Models;
using web_api_net5.Services;

namespace web_api_net5.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountCotroller : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountCotroller(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto dto)
        {
            _service.RegisterUser(dto);
            return Ok(dto);
        }

    }
}

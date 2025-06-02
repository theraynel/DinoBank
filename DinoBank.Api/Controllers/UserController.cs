using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinoBank.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("sample")]
        public IActionResult Sample() 
        {
            return Ok("Sample 001");
        }
    }
}

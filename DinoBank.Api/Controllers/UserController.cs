using DinoBank.Domain.User;
using DinoBank.Persistence.Database;
using Microsoft.AspNetCore.Mvc;

namespace DinoBank.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public UserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var users = _databaseService.GetAll();

            if (users.Count == 0 && users == null)
                return NotFound();
 
            return Ok(users);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _databaseService.GetAll();

            if (data.Count == 0 && data == null)
                return NotFound();

            var user = data.FirstOrDefault(x => x.Id == id);

            if(user == null)
                return NotFound();

            return Ok(user);
           
        }

        [HttpGet("get-by-userName/{userName}")]
        public IActionResult GetByUsername(string userName)
        {
            var data = _databaseService.GetAll();

            if (data.Count == 0 && data == null)
                return NotFound();

            var user = data.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return NotFound();

            return Ok(user);

        }

        [HttpGet("get-by-type/{type}")]
        public IActionResult GetByType(string type)
        {
            var data = _databaseService.GetAll();

            if (data.Count == 0 && data == null)
                return NotFound();

            var users = data.FindAll(x => x.Type == type);

            if (users.Count == 0 || users == null)
                return NotFound();

            return Ok(users);

        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] UserEntity user) 
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type))
                return BadRequest("Parametros no Validos");

            var data = _databaseService.Create(user);

            if (!data) 
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(data);
            
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] UserEntity user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type) ||
                user.Id <= 0)
                return BadRequest("Parametros no Validos");

            var data = _databaseService.Update(user);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(data);

        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Parametros no Validos");

            var data = _databaseService.Delete(id);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(data);

        }
    }
}

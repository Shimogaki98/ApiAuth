using ApiAuth.Models;
using ApiAuth.Repository;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult<dynamic>> AutenticateAsync([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { Message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = ""; // Ocultando a senha 

            return new
            {
                User = user,
                Token = token
            };
        }


    }
}

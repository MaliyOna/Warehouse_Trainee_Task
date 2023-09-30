using InnowiseProject.Database.Models;
using InnowiseProject.WebApi.DTO;
using InnowiseProject.WebApi.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Worker> userManager;
        private readonly JwtFactory jwtFactory;

        public AuthController(UserManager<Worker> userManager, JwtFactory jwtFactory)
        {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.UserName);

            if (user == null)
            {
                return NotFound("Такой пользователь не найден");
            }

            if (!await userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return BadRequest("Неверный пароль");
            }

            var role = (await userManager.GetRolesAsync(user)).First();

            string jwt = jwtFactory.GenerateToken(user.Id, role);

            return Ok(jwt);
        }
    }
}

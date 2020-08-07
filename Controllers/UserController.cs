using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopAPI.Data;
using EShopAPI.Models;
using EShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: Controller
    {
        [HttpPost]
        [Route("")]
        [Authorize(Roles="manager")]
        // [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser([FromServices] DataContext context, [FromBody] User model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await context.Users.AsNoTracking()
                .Where(x => x.Username.Equals(model.Username)&&x.Password.Equals(model.Password))
                .FirstOrDefaultAsync();

                if(user.Equals(null)) return NotFound(new { message = "Username or Password invalid!" });
                var token = TokenService.GenerateToken(user);

                return new { user = user, token = token };
            }
            catch(Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<dynamic>> ListUsers([FromServices] DataContext context, [FromBody] User model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return null;
        }

        //Restando os metodos PUT para atualizar o usuário e DELETE com [Authorize(Roles="manager")]

        [HttpGet]
        [Route("auth")]
        [Authorize]
        public async Task<ActionResult<string>> Auth() => "You're authorized";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<string>> AuthEmployee() => "You're an authorized Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<string>> AuthManager() => "You're an authorized Manager";
    }
}
using DapperProhect.Interface;
using DapperProhect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAuthService _userService;
        public IMemoryCache _memory { get; }

        public UserController(IAuthService userService, IMemoryCache memory)
        {
            _userService = userService;
            _memory = memory;
        }

        //Genarate Tokens
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthenticateModel model)
        {
            var user = await _userService.GenerateToken(model); ;

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            _memory.Set("Users", user);
            return Ok(new { user.Token,user.RefreshToken});
        }

        //Genarate Refresh Tokens
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string RefreshToken)
        {
            if (RefreshToken == null)
                return BadRequest("Invalid Request");

            var user = _memory.Get<User>("Users");
            TokenValidationParameters valid = _userService.GetTokenValidationParameter();

            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(user.Token, valid, out SecurityToken ValidatedToken);

            var UserName = principal.Identity.Name;

            if (user.RefreshToken == RefreshToken && user.Username==UserName)
            {
               var newToken= await _userService.GenaratedNewToken(user);
                if (newToken == null)
                    return BadRequest();
                _memory.Set("Users", newToken);
                return Ok(new { newToken.Token, newToken.RefreshToken });
            }

            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetallAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAlluser()
        {

            var user = _userService.GetAlluser();

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}

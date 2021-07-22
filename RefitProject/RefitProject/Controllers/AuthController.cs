using DapperProhect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Refit;
using RefitProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepositary auth;
        private readonly IMemoryCache cache;
        private static readonly string key = "GetToken";

        public AuthController(IAuthRepositary auth,IMemoryCache cache)
        {
            this.auth = auth;
            this.cache = cache;
        }
        [HttpPost("GetToken")]
        public async Task<IActionResult>Get(AuthenticateModel model)
        {
            var user = await auth.GetToken(model);
            var token = cache.Get(key);
            if (token != null)
                cache.Remove(key);
            cache.Set(key, user);
            return Ok();
        }

        [HttpGet("GetAllAdmins")]
        public async Task <IActionResult> GetAdmins()
        {
            var user = (User)cache.Get(key);
            if (user!=null)
            {
                var result = await auth.GetAdmins(user.Token);
                return Ok(result);
            }
            return Forbid();
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var user = (User)cache.Get(key);
            var result = await auth.GetUsers(user.Token);
            return Ok(result);
        }
    }
}

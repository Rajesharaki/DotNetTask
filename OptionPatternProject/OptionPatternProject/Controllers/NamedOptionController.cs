using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionPatternProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamedOptionController : ControllerBase
    {
        private readonly WeatherOption _option1;
        private readonly WeatherOption _option2;


        public NamedOptionController(IOptionsSnapshot<WeatherOption> option)
        {
            _option1 = option.Get("WeatherOption1");
           // _option2 = option.Get("WeatherOption2");
        }

        [HttpGet,Route("GetOption1")]
        public IActionResult GetOption1()
        {
            return Ok(new { _option1.Name, _option1.Display });
        }
        [HttpGet, Route("GetOption2")]
        public IActionResult GeGetOption2t()
        {
            return Ok(new { _option2.Name, _option2.Display });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionPatternProject.Interface;
using OptionPatternProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessage message;
        private readonly WeatherOption snapOption;
        private readonly WeatherOption option;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOptions<WeatherOption> option
            ,IOptionsSnapshot<WeatherOption> snapOption,IMessage message)
        {
            _logger = logger;
            this.message = message;
            this.option = option.Value;
            this.snapOption = snapOption.Value;

        }

        [HttpGet]
        public IActionResult GetOptionservice()
        {
            _logger.LogInformation("Entered GetOptionService method");
            return Ok(new { option.Name, option.Display });
        }

        [HttpGet,Route("GetOptionSnapshot")]
        public IActionResult GetOptionSnapshot()
        {
            _logger.LogInformation("Entered GetOptionSnapShot methos");
            return Ok(new { snapOption.Name,snapOption.Display});
        }

        [HttpGet,Route("GetOptionMoniter")]
        public IActionResult Get()
        {
            WeatherOption result = message.Get();
            return Ok(new { result.Name, result.Display });
        }
    }
}

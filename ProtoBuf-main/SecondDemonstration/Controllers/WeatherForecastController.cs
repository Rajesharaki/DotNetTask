using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecondDemonstration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44392/weatherforecast/");
            var response=await client.GetAsync("getproto");
            var comingbyte = await response.Content.ReadAsStreamAsync();
            //var stream = new MemoryStream(comingbyte);
            WeatherForecast result;
            try
            {
                result =Serializer.Deserialize<WeatherForecast>(comingbyte);
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return result.ToString(); ;
        }
    }
}

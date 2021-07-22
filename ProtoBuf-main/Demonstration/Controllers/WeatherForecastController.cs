using Google.Protobuf;
using GrpcService1;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demonstration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDistributedCache distributedCache, ILogger<WeatherForecastController> logger)
        {
            this.distributedCache = distributedCache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<WeatherForecast> Get()
        {
            WeatherForecast data = new WeatherForecast() {TemperatureC=30,Summary= "Nothing" };
            string d = await distributedCache.GetStringAsync("json");
            if (d != null)
            {
                var result=JsonConvert.DeserializeObject<WeatherForecast>(d);
                return result;
            }
            var serialized = JsonConvert.SerializeObject(data);
            var push = distributedCache.SetStringAsync("json", serialized);
            return data;
        }

        //protobuff
        [HttpGet, Route("getproto")]
        public  IActionResult Getproto()
        {
            var data = new WeatherForecast() { TemperatureC=30,Summary="Hello world" };
            //var stream = new MemoryStream();
            // Serializer.Serialize<WeatherForecast>(stream, data);
            //await this.distributedCache.SetAsync("protobuf", stream.ToArray());
            var result=data.ToByteArray();
            return File(result, "application/x-protobuf");
        }

        /////Message Pack
        //[HttpGet, Route("getMessage")]
        //public async Task<byte[]> getMessage()
        //{
        //    var rng = new Random();
        //    var data = new WeatherForeCastMessage() { Date = DateTime.Today, Summary = "Nothing", TemperatureC = 50 };
        //    var stream = new MemoryStream();
        //    MessagePackSerializer.Serialize<WeatherForeCastMessage>(stream, data);
        //    await this.distributedCache.SetAsync("getMessage", stream.ToArray());
        //    return stream.ToArray();
        //}
    }
}

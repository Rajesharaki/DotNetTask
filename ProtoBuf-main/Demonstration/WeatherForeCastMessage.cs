using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demonstration
{
    [MessagePackObject]
    public class WeatherForeCastMessage
    {
        [Key(0)]
        public DateTime Date { get; set; }
        [Key(1)]
        public int TemperatureC { get; set; }
        [Key(2)]
        public string Summary { get; set; }
    }


    //[ProtoContract]
    //public class WeatherForecast
    //{
    //    [ProtoMember(tag: 1)]
    //    public DateTime Date { get; set; }
    //    [ProtoMember(tag: 2)]
    //    public int TemperatureC { get; set; }
    //    [ProtoMember(tag: 3)]
    //    public string Summary { get; set; }
    //}
}

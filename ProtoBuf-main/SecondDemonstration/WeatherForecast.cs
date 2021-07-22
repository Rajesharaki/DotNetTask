using ProtoBuf;
using System;

namespace SecondDemonstration
{
    [ProtoContract]
    public class WeatherForecast
    {
        [ProtoMember(tag: 1)]
        public DateTime Date { get; set; }
        [ProtoMember(tag: 2)]
        public int TemperatureC { get; set; }
        [ProtoMember(tag: 3)]
        public string Summary { get; set; }
    }
}

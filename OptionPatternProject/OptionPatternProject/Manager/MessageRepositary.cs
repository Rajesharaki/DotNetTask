using Microsoft.Extensions.Options;
using OptionPatternProject.Interface;
using OptionPatternProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject.Manager
{
    public class MessageRepositary:IMessage
    {
        private  WeatherOption _option;

        public MessageRepositary(IOptionsMonitor<WeatherOption> option)
        {
            _option = option.CurrentValue;
            option.OnChange(config =>
            { 
                _option = config;
            });
        }

        public WeatherOption Get()
        {
            var result = new WeatherOption
            {
                Name = _option.Name,
                Display = _option.Display
            };

            return result;
        }
    }
}

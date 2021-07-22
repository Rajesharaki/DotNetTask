using OptionPatternProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject.Interface
{
    public interface IMessage
    {
        WeatherOption Get();
    }
}

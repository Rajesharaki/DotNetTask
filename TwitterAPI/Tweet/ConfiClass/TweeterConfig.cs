using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.ConfiClass
{
    public class TweeterConfig
    {
        public Dictionary<string, TweeterAuthConfig> Keys { get; set; } =
            new Dictionary<string, TweeterAuthConfig>(StringComparer.OrdinalIgnoreCase);
    }
}

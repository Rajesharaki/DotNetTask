using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireBasePushNotification
{
    public class IndexModel
    {
        private IWebHostEnvironment env;
        public string result;
        public IndexModel(IWebHostEnvironment env)
        {
            this.env = env;
        }
    }
}

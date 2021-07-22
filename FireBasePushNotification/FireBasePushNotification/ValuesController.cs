using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireBasePushNotification
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public ValuesController(IWebHostEnvironment envv)
        {
            this.env = envv;
        }
        [HttpGet]
        public async Task OnGetAsync()
        {
            var path = env.ContentRootPath;
            path = path + "\\Auth.json";
            FirebaseApp app = null;
            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                }, "myApp");
            }
            catch (Exception ex)
            {
                app = FirebaseApp.GetInstance("myApp");
            }

            var fcm = FirebaseMessaging.DefaultInstance;
            Message message = new Message()
            {
                Notification = new Notification
                {
                    Title = "My push notification title",
                    Body = "Content for this push notification"
                },
                Data = new Dictionary<string, string>()
                 {
                     { "AdditionalData1", "data 1" },
                     { "AdditionalData2", "data 2" },
                     { "AdditionalData3", "data 3" },
                 },

                Topic = "WebsiteUpdates"
            };

            var result = await fcm.SendAsync(message);
        }
    }
}

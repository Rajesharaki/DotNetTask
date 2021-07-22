using Microsoft.AspNetCore.Http;
using System;

namespace Tweets
{
    public class TweetModel
    {
        public string userName { get; set; }
        public string tweets { get; set; }
        public IFormFile file { get; set; }
        public byte[] imageFileByte { get; set; }
        public byte[] videoFileByte { get; set; }

    }
}

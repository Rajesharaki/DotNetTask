using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Tweets;
using Tweets.Interface;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetRepositary _tweetRepositary;
        private readonly IWebHostEnvironment _env;

        public TweetController(ITweetRepositary tweetRepositary, IWebHostEnvironment env)
        {
            this._tweetRepositary = tweetRepositary;
            this._env = env;
        }
        [HttpPost("PostTweet")]
        public async Task<IActionResult> PostTweet([FromForm] TweetModel twts)
        {
            if (twts.file != null)
            {
                var fileName = twts.file.FileName;
                var extension = Path.GetExtension(twts.file.FileName);
                if (!Directory.Exists(_env.WebRootPath + "//UploadedFiles//"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + "//UploadedFiles//");

                }
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                var path = Path.Combine(directory, fileName);
                if (extension == ".jpg" || extension == ".png")
                {
                    
                    try
                    {
                        using (FileStream Files = System.IO.File.Create(path))
                        {
                            twts.file.CopyTo(Files);
                        }
                        twts.imageFileByte = System.IO.File.ReadAllBytes(path);
                        System.IO.File.Delete(path);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                else
                {
                    using (FileStream Files = System.IO.File.Create(path))
                    {
                        twts.file.CopyTo(Files);
                    }
                    twts.videoFileByte = System.IO.File.ReadAllBytes(path);
                    System.IO.File.Delete(path);
                } 
            }

            await _tweetRepositary.PostTweet(twts);

            return Ok();
        }
    }
}

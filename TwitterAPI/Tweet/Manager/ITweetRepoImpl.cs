using System.Threading.Tasks;
using Tweetinvi;
using Tweets.Interface;
using Microsoft.Extensions.Options;
using TwitterAPI.ConfiClass;
using Tweetinvi.Parameters;
using System.IO;
using System.Text;

namespace Tweets.Manager
{
    public class ITweetRepoImpl : ITweetRepositary
    {
        private readonly TweeterConfig _tweeterConfig;

        public ITweetRepoImpl(IOptions<TweeterConfig> optionsSnapshot)
        {
            _tweeterConfig = optionsSnapshot.Value;
        }

        public ITweetRepoImpl(TweeterConfig obj)
        {
            _tweeterConfig = obj;
        }

        public async Task<string> PostTweet(TweetModel twts)
        {
            if (_tweeterConfig.Keys.TryGetValue(twts.userName, out var config))
            {
                var userClient = new TwitterClient(config.Key, config.SecretKey, config.Token, config.SecretToken);
                var user = await userClient.Users.GetAuthenticatedUserAsync();
                if (user != null)
                {
                    if (twts.file != null)
                    {
                        if (twts.imageFileByte != null)
                        {
                            var uploadedImage = await userClient.Upload.UploadTweetImageAsync(twts.imageFileByte);
                            var tweetWithImage = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters(twts.tweets)
                            {
                                Medias = { uploadedImage }
                            });
                        }
                        else if (twts.videoFileByte != null)
                        {
                            var uploadedVideo = await userClient.Upload.UploadTweetVideoAsync(twts.videoFileByte);
                            await userClient.Upload.WaitForMediaProcessingToGetAllMetadataAsync(uploadedVideo);

                            var tweetWithImage = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters(twts.tweets)
                            {
                                Medias = { uploadedVideo }
                            });
                        }
                        else
                        {
                            var uploadedImage = await userClient.Upload.UploadTweetImageAsync(Encoding.UTF8.GetBytes(twts.file.ToString()));
                            var tweetWithImage = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters(twts.tweets)
                            {
                                Medias = { uploadedImage }
                            });
                        }
                    }
                    else
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync(twts.tweets);
                    }
                    return "Successfull";

                }
                else
                {
                    return "Credentials invalid for " + twts.userName;
                }
              
            }
            else
            {
                return twts.userName + " Not found in Config file";
            }

        }
    }
}

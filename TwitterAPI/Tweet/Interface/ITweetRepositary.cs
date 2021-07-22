using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tweets.Interface
{
    public interface ITweetRepositary
    {
        Task<string> PostTweet(TweetModel twts);
    }
}

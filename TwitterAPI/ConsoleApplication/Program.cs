using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Tweets;
using Tweets.Manager;
using TwitterAPI;
using TwitterAPI.ConfiClass;

namespace ConsoleApplication
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var tweeterConfig = new TweeterConfig();

            configuration.GetSection(nameof(TweeterConfig)).Bind(tweeterConfig);
            var obj=Options.Create(tweeterConfig);


            var repositary = new ITweetRepoImpl(obj);
            var model = new TweetModel();
            Console.WriteLine("Enter User Name");
            model.userName = Console.ReadLine();
            Console.WriteLine("Please Enter the Message to post on tweet");
            model.tweets = Console.ReadLine();
            try
            {
                var result=await repositary.PostTweet(model);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
            Console.ReadLine();
        }
    }
}

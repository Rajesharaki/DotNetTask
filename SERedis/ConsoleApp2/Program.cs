using StackExchange.Redis;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer con = ConnectionMultiplexer.Connect("localhost");
            IDatabase cache = con.GetDatabase();
            cache.StringSet("foo", "bar");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

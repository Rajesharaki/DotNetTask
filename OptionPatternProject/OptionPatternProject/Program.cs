using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();

            //Console logging Provider(sink)
            logConfig.WriteTo.Console();

            //File logging Provide
            logConfig.WriteTo.Async(x=>
                x.File("Logs/log.log",
               /* outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.ffff} [{u3}] {Message:lj}{NewLine}{Exception}"*/
                fileSizeLimitBytes:200_000_000
                ,rollOnFileSizeLimit:true
                ,rollingInterval:RollingInterval.Day
                ,shared:true),100
            );

            //File Logging Provider in Json format
            logConfig.WriteTo.Async(x =>
                x.File(new CompactJsonFormatter(),
                "Logs/Json/log.json",
                fileSizeLimitBytes:200_000_000,
                rollOnFileSizeLimit:true,
                rollingInterval:RollingInterval.Day,
                shared:true
                ),100);

            //Seq Loggging Provider

            //logConfig.WriteTo.Async(x =>
            //x.Seq("http://localhost:5341/"
            //    ));

            Log.Logger = logConfig.CreateLogger();

            try
            {
                Log.Information("Starting up");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {

                Log.Fatal(ex.Message.ToString());
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureCustomJson)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();

        public  static void ConfigureCustomJson(HostBuilderContext context,IConfigurationBuilder config)
        {
            config.AddJsonFile("Custom.json");
        }
    }
}

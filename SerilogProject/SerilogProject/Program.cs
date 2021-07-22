using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Extensions.Logging;

namespace SerilogProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();


            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //return Host.CreateDefaultBuilder(args).ConfigureLogging((hostingContext, logging) => {
            //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.AddEventSourceLogger();
            //    // Enable NLog as one of the Logging Provider
            //    logging.AddNLog();
            //})
            //   .ConfigureWebHostDefaults(webBuilder =>
            //   {
            //       webBuilder.UseStartup<Startup>();
            //   });

            return Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration(ConfigureCustomJsonFile)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               }).UseSerilog();
        }
        public static void ConfigureCustomJsonFile(HostBuilderContext context, IConfigurationBuilder config)
        {
            config.AddJsonFile("CustomJsom.Json", optional: false, reloadOnChange: false);
            //config.AddXmlFile("web.config", optional: false, reloadOnChange: false);
        }
    }
}

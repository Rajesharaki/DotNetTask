using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OptionPatternProject.Interface;
using OptionPatternProject.Manager;
using OptionPatternProject.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPatternProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptionPatternProject", Version = "v1" });
            });

            //Console.WriteLine(Configuration.GetValue<String>("Student:Name"));

            services.Configure<WeatherOption>(Configuration.GetSection(nameof(WeatherOption)));

            services.AddSingleton<IMessage,MessageRepositary>();

            //services.Configure<WeatherOption>(Configuration.GetSection("WeatherOption"));
            //services.Configure<WeatherOption>(Configuration.GetSection("WeatherOptions"));

            services.Configure<WeatherOption>("WeatherOption1", Configuration.GetSection("WeatherOption"));
            //services.Configure<WeatherOption>("WeatherOption2", Configuration.GetSection("WeatherOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptionPatternProject v1"));
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging(option =>
            {
                option.MessageTemplate = "{Scheme},{RequestMethod},{Path},{Query},{StatusCode},{Elapsed:0.00}";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

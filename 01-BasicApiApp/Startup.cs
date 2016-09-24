using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }
        public string EnvironmentName { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() // Collection of sources for read/write key/value pairs
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            EnvironmentName = env.EnvironmentName;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters();
        }

        // This method gets called by the runtime, after ConfigureServices, and is required. Use this method to configure the HTTP request pipeline.
        // IApplicationBuilder is required; provides the mechanisms to configure an application’s request pipeline.
        // Middleware is configured here.
        public void Configure(IHostingEnvironment env, IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var startupLogger = loggerFactory.CreateLogger<Startup>();

            app.UseMvc();

            startupLogger.LogDebug("Debug output!");
            startupLogger.LogInformation("Application startup complete!");
            startupLogger.LogTrace("Trace output!");
            startupLogger.LogError("Error output!");
        }
    }
}
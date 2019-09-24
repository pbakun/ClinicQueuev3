using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            finally
            {
                log.Dispose();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://*:5001", "http://*:5000")
                .UseStartup<Startup>();
                //.UseSerilog();
    }
}

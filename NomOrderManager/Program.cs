using System;
using System.Configuration;
using Nancy.Hosting.Self;
using Serilog;

namespace NomOrderManager
{
    public static class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.RollingFile(@"logs\{Date}.txt", retainedFileCountLimit: 2)
                .CreateLogger();

            var config = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            var url = new Uri(ConfigurationManager.AppSettings["url"]);

            using (var host = new NancyHost(config, url))
            {
                host.Start();

                Log.Information("Nom Order Manager listening on {url}", url);

                Console.ReadLine();
                host.Stop();
            }
        }
    }
}

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;



namespace Webservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
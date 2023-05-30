using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using SportsClub.Models;

namespace SportsClub
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var WebHost = CreateHostBuilder(args).Build();

            RunMigration(WebHost);
            WebHost.Run();
        }

        private static void RunMigration(IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<SportsClubContext>();
                db.Database.Migrate();

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                });








    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EIM_assessment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EIM_assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitializeData();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitializeData()
        {
            var boards = new List<Board>
            {
                new Board{ Id = 1, CreatedAt = DateTime.Now.AddDays(-7), Name = "Board #1" },
                new Board{ Id = 2, CreatedAt = DateTime.Now.AddDays(-2), Name = "Board #2" },
                new Board{ Id = 3, CreatedAt = DateTime.Now.AddDays(-1), Name = "Board #3" }
            };

            var json = JsonConvert.SerializeObject(boards);

            var filePath = Application.Configuration["DataFile"];
            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, json);
        }
    }
}

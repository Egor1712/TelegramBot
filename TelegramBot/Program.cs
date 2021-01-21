using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Services.Bot;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bot.SetWebHook();
            Bot.StartReceiving();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
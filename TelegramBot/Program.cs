using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Services.Bot;

namespace TelegramBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Bot.SetWebHook();
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                                          {
                                              webBuilder.UseStartup<Startup>();
                                          });
    }
}
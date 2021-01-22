using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly JsonSerializer serializer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            serializer = JsonSerializer.Create();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ILogger<Startup> logger)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapPost(@"/Message/Post", async context =>
                                                   {
                                                       var json =await 
                                                           new StreamReader(context.Request.Body).ReadToEndAsync();
                                                       var reader =
                                                           new JsonTextReader(new StringReader(json));
                                                       var update =
                                                           serializer
                                                               .Deserialize<Update>(reader);
                                                       if (update?.Message is null)
                                                           return;
                                                       await Bot
                                                           .BotClientOnOnMessage(update
                                                               .Message);
                                                   });
                             });
        }
    }
}
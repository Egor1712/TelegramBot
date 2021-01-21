using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> logger;

        public MessageController(ILogger<MessageController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async void Post([FromBody]Update update)
        {
            if (update is null)
            {
                logger.LogInformation("Update was null");
                foreach (var requestHeader in Request.Headers)
                {
                    logger.LogDebug($"{requestHeader.Key} : {requestHeader.Value}");
                }
                return;
            }
            var message = update.Message;
            logger.LogInformation($"Update was received, message : {message}");
            await Bot.BotClientOnOnMessage(message);
        }
    }
}
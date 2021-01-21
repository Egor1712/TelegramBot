using System.IO;
using System.Text;
using AngleSharp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;
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
               logger.LogError("update is null!");
                return;
            }
            var message = update.Message;
            if (message is null)
            {
                logger.LogError("message is null");
                return;
            }
            await Bot.BotClientOnOnMessage(message);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Services.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    public class MessageController : Controller
    {
        [HttpPost]
        public void Post([FromBody]Update update)
        {
            if (update is null)
                return;
            var message = update.Message;
            Bot.BotClientOnOnMessage(message).Wait();
        }
    }
}
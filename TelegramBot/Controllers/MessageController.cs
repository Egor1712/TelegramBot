using Microsoft.AspNetCore.Mvc;
using Services.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    public class MessageController : Controller
    {
        [HttpPost]
        public async void Post([FromBody]Update update)
        {
            var message = update.Message;
            await Bot.BotClientOnOnMessage(message);
        }
    }
}
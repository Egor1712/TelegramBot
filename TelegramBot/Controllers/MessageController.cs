using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    public class MessageController : Controller
    {
        [Route("update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            await Bot.BotClientOnOnMessage(update.Message);
            return Ok();
        }
    }
}
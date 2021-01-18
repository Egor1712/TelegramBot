
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Bot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class MessageController : ControllerBase
    {
        private readonly Services.Bot.Bot bot = new Services.Bot.Bot();
        
        [HttpGet]
        public string Get()
        {
            return "Hello there!";
        }
    }
}
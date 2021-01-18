
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class StartCommand : ICommand
    {
        public string Description => "Command /start allows to start work with bot";
        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            await bot.SendMessageAsync(message.Chat.Id, "List of commands - /help");
        }
    }
}
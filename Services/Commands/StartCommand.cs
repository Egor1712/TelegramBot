
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class StartCommand : ICommand
    {
        public string Description => "Command /start allows to start work with bot";

        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            await client.SendTextMessageAsync(message.Chat.Id, "List of commands - /help");
        }
    }
}
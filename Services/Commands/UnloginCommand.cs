using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class UnloginCommand : ICommand
    {
        public string Description => "Command /unlogin drop your data";
        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            user = new User();
            await client.SendTextMessageAsync(message.Chat.Id,
                                                 "Please send me your login and password in format <login> : <password>");
            
        }
    }
}
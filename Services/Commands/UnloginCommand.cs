using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class UnloginCommand : ICommand
    {
        public string Description => "Command /unlogin drop your data";

        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            bot.UnloginUserByChatId(message.Chat.Id);
            await bot.SendMessageAsync(message.Chat.Id,
                                       "Please send me your login and password in format <login> : <password>");
        }
    }
}
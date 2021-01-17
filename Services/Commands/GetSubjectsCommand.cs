using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Services.Commands
{
    public class GetSubjectsCommand : ICommand
    {
        public string Description => "Command /subjects gets your subjects and rates";

        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            foreach (var subject in user.Subjects)
            {
                await client.SendTextMessageAsync(message.Chat.Id, subject.ToString(),
                                                  ParseMode.Default);
            }
        }
    }
}
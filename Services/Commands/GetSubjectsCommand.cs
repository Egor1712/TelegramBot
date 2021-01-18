using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetSubjectsCommand : ICommand
    {
        public string Description => "Command /subjects gets your subjects and rates";

        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            foreach (var subject in user.Subjects)
            {
                await bot.SendMessageAsync(message.Chat.Id, subject.ToString());
            }
        }
    }
}
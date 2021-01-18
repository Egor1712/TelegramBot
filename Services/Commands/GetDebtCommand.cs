using System.Globalization;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetDebtCommand : ICommand
    {
        public string Description => "Command /debt gets your dormitory debt";

        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            await bot.SendMessageAsync(message.Chat.Id,
                                       user.DormitoryService.Debt.ToString(CultureInfo
                                           .InvariantCulture));
        }
    }
}
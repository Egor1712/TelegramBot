using System.Globalization;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetDebtCommand : ICommand
    {
        public string Description => "Command /debt gets your dormitory debt";

        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            await client.SendTextMessageAsync(message.Chat.Id,
                                              user.DormitoryService.Debt.ToString(CultureInfo
                                                  .InvariantCulture));
        }
    }
}
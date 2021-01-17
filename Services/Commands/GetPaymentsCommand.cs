using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetPaymentsCommand : ICommand
    {
        public string Description => "Command /payments gets your dormitory payments";

        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            if (user.DormitoryService.Extractions.Count == 0)
            {
                await client.SendTextMessageAsync(message.Chat.Id, " You haven't any payments =(");
                return;
            }
            
            foreach (var extraction in user.DormitoryService.Extractions)
            {
                await client.SendTextMessageAsync(message.Chat.Id, extraction.ToString());
            }
        }
    }
}